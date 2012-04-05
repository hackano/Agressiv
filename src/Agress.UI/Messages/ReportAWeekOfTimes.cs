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
using Agress.Messages.Commands;

namespace Agress.UI.Messages
{
	internal class ReportAWeekOfTimes
		: Agress.Messages.Commands.ReportAWeekOfTimes
	{
		public ReportAWeekOfTimes(string description, IEnumerable<double> weekHours, AccountingData accountingData)
		{
			Description = description;
			WeekHours = weekHours;
			Data = accountingData;
			SaveChanges = true;
		}

		public string Description { get; private set; }
		public IEnumerable<double> WeekHours { get; private set; }
		public AccountingData Data { get; private set; }
		public bool SaveChanges { get; private set; }
	}
}