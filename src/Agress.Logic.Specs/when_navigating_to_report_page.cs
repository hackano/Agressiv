using System;
using System.Linq;
using Agress.Core.Commands;
using NUnit.Framework;

namespace Agress.Logic.Specs
{
	public class when_navigating_to_report_page
	{
		private MainPresenter driver;

		[SetUp]
		public void Setup()
		{
			driver = new MainPresenter(
				"henrikfeldt",
				Password.String,
				"DS",
				"https://economy.waygroup.se/agresso/System/Login.aspx"
				);
		}

		[Test, STAThread]
		public void Try_report_week()
		{
			driver.Consume(new ReportAWeekOfTimes(
			               	"Löpande",
			               	new[] {4.5, 7, 8, 9}.ToList(),
							new AccountingData()
			               	));
		}

		[Test, STAThread]
		public void Try_report_day()
		{
			driver.Consume(new ReportTimesForADay(DayOfWeek.Monday, 5.6, "Löpande", 
				"I did a spike with to make Rhino Security into a message-oriented service.",
				new AccountingData()));
		}

		[Test]
		public void NAME()
		{
			Console.WriteLine((int)DayOfWeek.Monday);
		}
	}
}