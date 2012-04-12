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
		const string VoucherNo = "560000";
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
				var fullPath = Path.GetFullPath(@"C:\Users\xyz\Dropbox\Dropbox-Jayway\Reseräkning\2011-01-29 - The Queens Head - KKväll.jpg");
				var scanPath = new Uri(string.Format("file:///{0}",
					Uri.EscapeUriString(fullPath.Replace("\\", "/"))));
				File.Exists(scanPath.LocalPath).ShouldBeTrue();

				mailScenario = TestFactory.ForConsumer<MailSender>()
					.InSingleBusScenario()
					.New(x =>
						{
							//x.UseRabbitMqBusScenario(); // see https://github.com/MassTransit/MassTransit/issues/114
							x.ConstructUsing(() => new MailSender(sender, new SystemProcessManager()));
							x.Send<Messages.Events.KnowledgeActivityRegistered>(new KnowledgeActivityRegistered
								{
									VoucherNumber = VoucherNo,
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

		// seems like octet-stream is preferred by mail clients

		//It should_have_pdf_attachment = () =>
		//    msg.Attachments
		//        .Any(x => x.ContentType.MediaType.Equals("application/x-pdf"))
		//        .ShouldBeTrue();

		//It should_have_image_attachment = () =>
		//    msg.Attachments
		//        .Any(x => x.ContentType.MediaType.Equals("image/jpeg"))
		//        .ShouldBeTrue();

		It should_have_proper_names = () =>
			msg.Attachments
				.ToList()
				.ForEach(a => a.Name.ShouldNotBeEmpty());

		static Attachment FindAttachment(string namePart)
		{
			return msg.Attachments.First(x => x.Name.Contains(namePart));
		}

		// https://connect.microsoft.com/VisualStudio/feedback/details/696372/filename-encoding-error-when-encoding-utf-8-and-encoded-name-exceeds-the-length-of-a-single-mime-header-line#details
		//It should_give_the_scan_its_real_name = () =>
		//    FindAttachment("jpg").Name.ShouldEqual("2011-01-29 - The Queens Head - KKväll.jpg");

		It should_give_pdf_real_name = () =>
			FindAttachment("pdf").Name.ShouldEqual(VoucherNo + ".pdf");
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