using System;
using System.IO;
using System.Threading;
using Agress.Logic;
using Agress.Logic.Framework;
using Agress.Logic.Pages;
using Agress.Messages.Commands;
using Agress.Messages.Events;
using MassTransit;
using MassTransit.Util;
using NLog;
using WatiN.Core;

namespace Agress.WebDriver
{
	public class KnowledgeActivityListener
		: Consumes<IConsumeContext<RegisterKnowledgeActivityExpense>>.All
	{
		readonly IServiceBus _bus;
		static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public KnowledgeActivityListener([NotNull] IServiceBus bus)
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
						_logger.Debug("logging in");
						var login = browser.GoToPage<LoginPage>();
						login.LogIn(new EnvironmentCredentials(
						            	context.Headers["AGRESSO_USERNAME"],
						            	context.Headers["AGRESSO_PASSWORD"]));

						var expense = browser.GoToPage<ExpenseClaimPage>();
						expense.ExpenseType.Select(PageStrings.ExpenseClaimPage_TypeOfExpense_Expense);
						expense.Cause.Value = message.Cause;
						expense.Comment.Value = message.Comment;
						expense.Next1.Click();

						expense.AddSimpleExpense("REPTOTINT", message.Epoch.AsDateTime(),
						               message.Comment, message.Amount);
						expense.DoneWithRows();
						expense.Next1.Click();

						if (message.SubmitFinal)
							expense.SubmitFinal();
						else 
							expense.SubmitDraft();

						using (var ms = new MemoryStream())
						{
							using (var popup = expense.SaveSupportingDocuments(ms))
							{
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
					}
				}));
			t.SetApartmentState(ApartmentState.STA);
			t.Start();
			t.Join();
		}
	}
}