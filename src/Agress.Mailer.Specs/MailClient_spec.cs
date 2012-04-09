using System.Net.Mail;

#pragma warning disable 169
// ReSharper disable InconsistentNaming

namespace Agress.Mailer.Specs
{
	using System;
	using Machine.Specifications;

	// machine specifications howto:
	// https://github.com/machine/machine.specifications
	// sample: https://github.com/joliver/EventStore/blob/master/src/tests/EventStore.Core.UnitTests/OptimisticEventStreamTests.cs

	[Subject(typeof (MailClient))]
	public class When_sending_email
	{
		static MailClient subject;

		Establish context = () =>
			{
				subject = new MailClientImpl();
			};

		Because of = () =>
			{
				var message = new MailMessage();
				message.Body = "Test e-mail from spec";
				message.Subject = "Test subject";

				exception = Catch.Exception(() => subject.Send(message, "spec"));
			};

		static Exception exception;

		It should_not_throw_exception = () =>
			exception.ShouldBeNull();
	}
}