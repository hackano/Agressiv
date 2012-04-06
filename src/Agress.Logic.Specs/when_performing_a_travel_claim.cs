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

using System.Linq;
using Agress.Logic.Specs.Framework;
using Agress.Logic.Specs.Messages;
using Agress.Messages.Commands;
using MassTransit;
using Moq;
using NUnit.Framework;

namespace Agress.Logic.Specs
{
	public class when_performing_a_travel_claim
	{
		private MainPresenter _driver;
		private readonly Mock<IServiceBus> _mockBus = new Mock<IServiceBus>();

		[SetUp]
		public void Setup()
		{
			var creds = TestFactory.GetCredentialsFromFileOrEnv();

			_driver = new MainPresenter(
				_mockBus.Object,
				creds.UserName,
				creds.Password,
				"DS",
				"https://economy.waygroup.se/agresso/System/Login.aspx"
				);
		}

		[Test]
		public void Try_report_week()
		{
			_driver.Consume(new ReportWeekTimes(
								"Löpande",
								new[] { 4.5, 7, 8, 9 }.ToList(),
								new AccountingData()
								));
		}
	}
}