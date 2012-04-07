// Copyright 2012 Henrik Feldt
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.

using System;
using System.IO;
using System.Threading;
using Agress.Logic;
using Agress.Logic.Pages;
using Agress.Messages.Commands;
using Agress.Messages.Events;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MassTransit;
using MassTransit.NLogIntegration;
using MassTransit.Util;
using NLog;
using WatiN.Core;
using Component = Castle.MicroKernel.Registration.Component;
using Agress.Logic.Framework;

namespace Agress.WebDriver
{
	internal class Program
	{
		IServiceBus _bus;
		IWindsorContainer _container;

		public Program()
		{
			_container = new WindsorContainer()
				.Install(new CommandListeners());
		}

		static void Main(string[] args)
		{
		}

		void Stop()
		{
			if (_container != null)
				_container.Release(_bus);
		}

		void Run()
		{
			_bus = ServiceBusFactory.New(sbc =>
				{
					sbc.UseNLog();

					sbc.ReceiveFrom("rabbitmq://localhost/Agressiv.WebDriver");

					sbc.Subscribe(s =>
						{
							var sett = Settings.Default;
							s.Consumer(() =>
							           new MainPresenter(_bus,
							                             sett.Login_Username,
							                             sett.Login_Password,
							                             sett.Login_Client,
							                             sett.Login_Url
							           	));
						});

					sbc.UseRabbitMqRouting();
				});
		}
	}

	public class KnowledgeEventListener
		: Consumes<IConsumeContext<RegisterKnowledgeActivityExpense>>.All
	{
		readonly IServiceBus _bus;
		static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public KnowledgeEventListener([NotNull] IServiceBus bus)
		{
			if (bus == null) throw new ArgumentNullException("bus");
			_bus = bus;
		}

		public void Consume(IConsumeContext<RegisterKnowledgeActivityExpense> context)
		{
			var message = context.Message;

			var t = new Thread(new ThreadStart(delegate
				{
					using (var browser = new IE())
					{
						var login = browser.GoToPage<LoginPage>();
						login.LogIn(new EnvironmentCredentials(
							context.Headers["AGRESSO_USERNAME"],
							context.Headers["AGRESSO_PASSWORD"]));

						var expense = browser.GoToPage<ExpenseClaimPage>();
						expense.ExpenseType.Select(PageStrings.ExpenseClaimPage_TypeOfExpense_Expense);
						expense.Cause.Value = message.Cause;
						expense.Comment.Value = message.Comment;
						expense.Next1.Click();

						expense.AddRow("REPTOTINT", message.Epoch.AsDateTime(),
							message.Comment, message.Amount);
						expense.DoneWithRows();
						expense.Next1.Click();

						if (message.SubmitFinal)
							expense.SubmitFinal();
						else 
							expense.SubmitDraft();

						using (var ms = new MemoryStream())
						{
							var popup = expense.SaveSupportingDocuments(ms);

							var evt = new KnowledgeActivityReplyEvent
								{
									Period = popup.Period, 
									VoucherNumber = popup.VoucherNo, 
									Voucher = ms.ToArray()
								};

							context.Respond<KnowledgeActivityRegistered>(evt);
							_bus.Publish(evt);
						}
					}
				}));
			t.SetApartmentState(ApartmentState.STA);
			t.Start();
			t.Join();
		}

	}

	public class KnowledgeActivityReplyEvent
		: KnowledgeActivityRegistered
	{
		public string VoucherNumber { get; set; }
		public string Period { get; set; }
		public byte[] Voucher { get; set; }
	}

	static class DateTimeEx
	{
		public static DateTime AsDateTime(this long unixTime)
		{
			var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return epoch.AddSeconds(unixTime);
		}
	}

	internal class CommandListeners : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<KnowledgeEventListener>());
		}
	}
}