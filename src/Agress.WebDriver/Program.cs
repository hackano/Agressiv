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
using System.Threading;
using Agress.Logic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MassTransit;
using MassTransit.NLogIntegration;
using Topshelf;
using Component = Castle.MicroKernel.Registration.Component;

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
			Thread.CurrentThread.Name = "Mailer Main Thread";

			HostFactory.Run(x =>
			{
				x.Service<Program>(s =>
				{
					s.ConstructUsing(name => new Program());
					s.WhenStarted(p => p.Start());
					s.WhenStopped(p => p.Stop());
					s.WhenContinued(p => p.Start());
					s.WhenPaused(p => p.Stop());
				});

				x.RunAsNetworkService();

				x.SetDescription("Performs reporting of expenses, by driving the browser through the Agresso UI");
				x.SetDisplayName("Agressiv.WebDriver");
				x.SetServiceName("Agressiv.WebDriver");
			});
		}

		void Stop()
		{
			if (_container != null)
				_container.Release(_bus);
		}

		void Start()
		{
			_bus = ServiceBusFactory.New(sbc =>
				{
					sbc.UseNLog();

					sbc.ReceiveFrom("rabbitmq://localhost/Agressiv.WebDriver");

					sbc.Subscribe(s => s.LoadFrom(_container));

					sbc.UseRabbitMqRouting();
				});
		}
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
			container.Register(Component.For<KnowledgeActivityListener>());
		}
	}
}