using System;
using System.Linq;
using System.Threading;
using Agress.Core;
using Agress.Core.Commands;
using Agress.Core.Events;
using MassTransit;
using Moq;
using NUnit.Framework;

namespace Agress.Logic.Specs
{
	//[TestFixture(ApartmentState = ApartmentState.STA)] 
	public class when_navigating_to_report_page
	{
		private MainPresenter driver;
		private Mock<IServiceBus> _MockBus = new Mock<IServiceBus>();


		[SetUp]
		public void Setup()
		{
			driver = new MainPresenter(
				_MockBus.Object,
				"henrikfeldt",
				Password.String,
				"DS",
				"https://economy.waygroup.se/agresso/System/Login.aspx"
				);
		}

		[Test]
		public void Try_report_week()
		{
			driver.Consume(new ReportAWeekOfTimes(
			               	"Löpande",
			               	new[] {4.5, 7, 8, 9}.ToList(),
							new AccountingData()
			               	));
		}

		[Test]
		public void Try_report_day()
		{
			_MockBus.Setup(x => x.Publish((SingleDayTimeReported)null, null))
				.Verifiable();

			driver.Consume(new ReportTimesForADay(DayOfWeek.Monday, 5.6, "Löpande", 
				"I did a spike with to make Rhino Security into a message-oriented service.",
				new AccountingData()));

			//_MockBus.Verify(x => x.Publish(It.IsAny<SingleDayTimeReported>(), It.Is<>(_ => true)));
		}

		[Test]
		public void NAME()
		{
			Console.WriteLine((int)DayOfWeek.Monday);
		}
	}
}