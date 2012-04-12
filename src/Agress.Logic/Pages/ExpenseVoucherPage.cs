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
using WatiN.Core;

namespace Agress.Logic.Pages
{
	public class ExpenseVoucherPage
		: Page, IDisposable
	{
		Browser _b;

		public void SetBrowser(Browser b)
		{
			_b = b;
		}

		public string VoucherNo
		{
			get
			{
				return Document
					.Element(Find.ByText(PageStrings.ExpenseClaimPrintOut_VoucherNoAbbr))
					.NextSibling
					.Text;
			}
		}

		public string Period
		{
			get
			{
				return Document
					.TableCell(Find.ByText(PageStrings.ExpenseClaimPrintOut_PeriodAbbr))
					.NextSibling.Text;
			}
		}

		public void Dispose()
		{
			if (_b != null)
			{
				_b.Close();
				_b.Dispose();
			}
		}
	}
}