using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using FakeItEasy;
using Machine.Specifications;
using MassTransit;
using MassTransit.Testing;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

namespace Agress.Mailer.Specs
{
	[Subject(typeof (MailSender))]
	public class When_receiving_reported_event
	{
		static ConsumerTest<BusTestScenario, MailSender> mailScenario;
		static MailClient sender;
		static byte[] htmlPage;
		static MailMessage msg;

		Establish context = () =>
			{
				htmlPage = File.ReadAllBytes("Michel_Foucault.htm");
				
				sender = A.Fake<MailClient>();

				A.CallTo(() => sender.Send(A<MailMessage>.Ignored))
					.Invokes(call =>
						{
							msg = call.Arguments[0] as MailMessage;
						});
			};

		Because of = () =>
			{
				mailScenario = TestFactory.ForConsumer<MailSender>()
					.InSingleBusScenario()
					.New(x =>
						{
							x.UseRabbitMqBusScenario();
							x.ConstructUsing(() => new MailSender(sender));
							x.Send(new KnowledgeActivityRegistered
								{
									VoucherNumber = "560000",
									Period = "24677241",
									Voucher = htmlPage
								},
								(scenario, context) => context.SendResponseTo(scenario.Bus));
						});

				mailScenario.Execute();
			};

		It should_called_send = () => 
			A.CallTo(() => sender.Send(A<MailMessage>.Ignored))
				.MustHaveHappened(Repeated.Exactly.Once);

		It should_have_sent_with_attachment = () =>
			msg.Attachments.Count
				.ShouldEqual(1);

		It should_have_pdf_attachment = () => 
			msg.Attachments[0]
				.ContentType
				.ShouldEqual(new ContentType("application/x-pdf"));
	}

	// test messages
	class KnowledgeActivityRegistered
		: Messages.Events.KnowledgeActivityRegistered
	{
		public string VoucherNumber { get; set; }
		public string Period { get; set; }
		public byte[] Voucher { get; set; }
	}
}