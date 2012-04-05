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

using System.Collections.Generic;
using System.Linq;
using Agress.Logic.Specs.Messages;
using Agress.Messages.Commands;
using MassTransit;
using Moq;
using NUnit.Framework;

namespace Agress.Logic.Specs
{
	public class when_navigating_to_report_page
	{
		private MainPresenter _driver;
		private readonly Mock<IServiceBus> _MockBus = new Mock<IServiceBus>();

		[SetUp]
		public void Setup()
		{
			var creds = TestFactory.GetCredentialsFromFileOrEnv();

			_driver = new MainPresenter(
				_MockBus.Object,
				creds.Username,
				creds.Password,
				"DS",
				"https://economy.waygroup.se/agresso/System/Login.aspx"
				);
		}

		[Test, RequiresSTA]
		public void Try_report_week()
		{
			_driver.Consume(new ReportWeekTimes(
			                	"Löpande",
			                	new[] {4.5, 7, 8, 9}.ToList(),
			                	new AccountingData()
			                	));
		}
	}

	namespace Messages
	{
		internal class ReportWeekTimes
			: ReportAWeekOfTimes
		{
			public ReportWeekTimes(string description, IEnumerable<double> weekHours, AccountingData data)
			{
				Description = description;
				WeekHours = weekHours;
				Data = data;
			}

			public string Description { get; private set; }
			public IEnumerable<double> WeekHours { get; private set; }
			public AccountingData Data { get; private set; }
			public bool SaveChanges { get; private set; }
		}
	}
}