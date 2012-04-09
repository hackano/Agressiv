using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using Agress.Messages.Mailer;
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

		static byte[] htmlPage;
		static MailMessage msg;

		static MailClient sender;
		static ProcessManager procMgr;

		Establish context = () =>
			{
				htmlPage = File.ReadAllBytes("CutyCapt.html");
				
				sender = A.Fake<MailClient>();
				procMgr = A.Fake<ProcessManager>();

				A.CallTo(() => sender.Send(A<MailMessage>.Ignored, A<string>.Ignored))
					.Invokes(call =>
						{
							msg = call.Arguments[0] as MailMessage;
						});

				A.CallTo(() => procMgr.Start(A<string>.Ignored, A<string[]>.Ignored))
					.Returns(A.Fake<IProcess>());
			};

		Cleanup after_test = () => 
			mailScenario.Dispose();

		Because of = () =>
			{
				var fullPath = Path.GetFullPath("work nap.jpg");
				var scanPath = new Uri(string.Format("file:///{0}", 
					fullPath.Replace("\\", "/")));
				File.Exists(scanPath.LocalPath).ShouldBeTrue();

				mailScenario = TestFactory.ForConsumer<MailSender>()
					.InSingleBusScenario()
					.New(x =>
						{
							//x.UseRabbitMqBusScenario(); // see https://github.com/MassTransit/MassTransit/issues/114
							x.ConstructUsing(() => new MailSender(sender, new SystemProcessManager()));
							x.Send<Messages.Events.KnowledgeActivityRegistered>(new KnowledgeActivityRegistered
								{
									VoucherNumber = "560000",
									//Period = "24677241",
									Voucher = htmlPage,
									UserName = "My Spec User",
									Scan = scanPath
								},
								(scenario, context) => context.SendResponseTo(scenario.Bus));
						});

				mailScenario.Execute();
			};

		It should_have_received_message = () =>
			mailScenario.Received.Any<Messages.Events.KnowledgeActivityRegistered>();

		It should_have_trigged_sent_email_event = () =>
			mailScenario.Published
				.Any<MailSent>()
				.ShouldBeTrue();

		It should_called_send = () => 
			A.CallTo(() => sender.Send(A<MailMessage>.Ignored, A<string>.Ignored))
				.MustHaveHappened(Repeated.Exactly.Once);

		It should_have_sent_with_attachment = () =>
			msg.Attachments.Count
				.ShouldEqual(2);

		It should_have_pdf_attachment = () =>
			msg.Attachments
				.Any(x => x.ContentType.Equals(new ContentType("application/x-pdf")))
				.ShouldBeTrue();

		It should_have_image_attachment = () =>
			msg.Attachments
				.Any(x => x.ContentType.Equals(new ContentType("image/jpeg")))
				.ShouldBeTrue();
	}

	// test messages
	class KnowledgeActivityRegistered
		: Messages.Events.KnowledgeActivityRegistered
	{
		public string VoucherNumber { get; set; }
		public byte[] Voucher { get; set; }
		public string UserName { get; set; }
		public Uri Scan { get; set; }
	}
}