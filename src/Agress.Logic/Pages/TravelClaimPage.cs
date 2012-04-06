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

using System;
using Agress.Logic.Framework;
using WatiN.Core;

namespace Agress.Logic.Pages
{
	[PageFromDriver(typeof(NavigateToExpenseClaimPage))]
	public class ExpenseClaimPage
		: Page
	{
	}

	public class NavigateToExpenseClaimPage
		: Driver
	{
		public void Drive(Browser b)
		{
			Func<LeftMenu> lm = () => b.Frame(AgressoNamesAndIds.MenuFrameId).Page<LeftMenu>();

			var timeAndExpenses = lm().TimeAndExpenses;

			timeAndExpenses.Click();

			var findPlus = Find.BySrc(s => s.Contains("Plus.gif"));

			var plusLink = b.Frame(AgressoNamesAndIds.MenuFrameId).Image(findPlus);

			while (plusLink.Exists)
			{
				plusLink.Click();
				plusLink = b.Frame(AgressoNamesAndIds.MenuFrameId).Image(findPlus);
			}

			var expenseClaim = b.Frame(AgressoNamesAndIds.MenuFrameId).Link(Find.ByText(PageStrings.LeftMenuPage_ExpenseClaim));
			expenseClaim.Click();
		}
	}
}