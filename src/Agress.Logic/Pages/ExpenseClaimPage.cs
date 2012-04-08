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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Agress.Logic.Framework;
using WatiN.Core;
using System.Linq;

namespace Agress.Logic.Pages
{
	// I hate that this page is so long; but it is really only 
	// a single responsibility in it; the expense page.

	// I can only imagine how long the code-behind page of the ASPX file
	// is in Agresso!! Programmers must go there to die!

	[PageFromDriver(typeof(NavigateToExpenseClaimPage))]
	public class ExpenseClaimPage
		: Page
	{
		readonly CultureInfo _swedishCulture = new CultureInfo("sv-SE");

		#region First Tab

		public TextField Cause
		{
			get { return Document.TextField("b_s5_l1s5_ctl00_ext_inv_ref_i"); }
		}

		public TextField Comment
		{
			get { return Document.TextField("b_s5_l1s5_ctl00_comment_i"); }
		}

		public SelectList ExpenseType
		{
			get { return Document.SelectList("b_s5_l1s5_ctl00_travel_type_i"); }
		}

		#endregion

		public Button Next1
		{
			get { return Document.Button("b__pageButtons__wizNext"); }
		}

		public Div ActiveTab
		{
			get { return Document.Div(Find.ByClass("TabSelected")); }
		}

		#region Third Tab

		public IEnumerable<ExpenseRow> ExpenseTable
		{
			get
			{
				return Document.Table("b_g3s14")
					.TableRows.Filter(row => !row.ClassName.Equals("Header"))
					.TakeWhile(row => !row.ClassName.Equals("SumItem"))
					.Select((r, index) =>
						new ExpenseRow
							{
								Type = r.Div(string.Format("b_g3s14__row{0}_ctl00_c", index)).Text,
								Description = r.Div(string.Format("b_g3s14__row{0}_ctl02_c", index)).Text,
								Multiplier = double.Parse(r.Div(string.Format("b_g3s14__row{0}_ctl03_c", index)).Text),
								Amount = double.Parse(r.Div(string.Format("b_g3s14__row{0}_ctl04_c", index)).Text),
								Getter = () => Document.TableRow(r.Id)
							});
			}
		}

		Button AddRowButton
		{
			get { return Document.Button("b_g3s14__buttons__newButton"); }
		}

		SelectList ExpenseTypeThirdTab
		{
			get { return Document.SelectList("b_s15_l3s15_ctl00_expense_type_i"); }
		}

		TextField Date
		{
			get { return Document.TextField("b_s15_l3s15_ctl00_date_from_i"); }
		}

		TextField ExpenseDesc
		{
			get { return Document.TextField("b_s15_l3s15_ctl00_description_i"); }
		}

		TextField Amount
		{
			get { return Document.TextField("b_s15_l3s15_ctl00_amount_i"); }
		}

		#endregion

		public void AddSimpleExpense(string selectValue,
			DateTime expenseDate, string expenseDescription, 
			double expenseAmount)
		{
			if (Math.Abs(expenseAmount - 0.0) < 0.0001)
				throw new ArgumentException("Zero amount", "expenseAmount");
			
			AddRowButton.Click();
			ExpenseTypeThirdTab.SelectByValue(selectValue);
			Date.Value = expenseDate.ToString("d", _swedishCulture);

			if (!string.IsNullOrWhiteSpace(expenseDescription))
				ExpenseDesc.Value = expenseDescription;
			
			Amount.Value = expenseAmount.ToString("0.00", _swedishCulture);
		}

		private TextField ProjectForExpense
		{
			get { return Document.TextField("b_s15_s53_l15s53_ctl00_dim_2_i"); }
		}

		public void AddRepresentationInternal(DateTime expenseDate,
			string expenseDescription, double expenseAmount)
		{
			AddSimpleExpense("REPTOTINT", expenseDate, expenseDescription, expenseAmount);
			ProjectForExpense.Value = 10003.ToString(_swedishCulture);
			UpdateAllPosts.Click();
		}

		public class ExpenseRow
		{
			public string Type { get; set; }
			public string Description { get; set; }
			public double Multiplier { get; set; }
			public double Amount { get; set; }
			public Func<TableRow> Getter { get; set; }
		}

		/// <summary>
		/// Clicks the 'Update posts' button that allows you to move to the next view.
		/// </summary>
		public void DoneWithRows()
		{
			AccountingGroup.Click();
			UpdateAllPosts.Click();
		}

		private TableCell AccountingGroup
		{
			get
			{
				return Document.Span(span =>
				                     !string.IsNullOrWhiteSpace(span.Text)
				                     && span.Text.Contains(PageStrings.ExpenseClaimPage_AccountingLabel))
					.FindParent<TableCell>();
			}
		}

		private Button UpdateAllPosts
		{
			get { return Document.Button("b_s15_s53__cb53-428"); }
		}

		#region Fourth Tab

		public void SubmitDraft()
		{
			SubmitWithValue("0");
		}

		public void SubmitFinal()
		{
			SubmitWithValue("1");
		}

		void SubmitWithValue(string selectedValue)
		{
			Document.SelectList("b_s16_l4s16_ctl00_confirm_flag_descr_i")
				.SelectByValue(selectedValue);

			Document.Div(Find.ByName("b$_item_save"))
				.Click();
		}

		#endregion

		#region Saved Info Block

		public string InfoBlock
		{
			get
			{
				return Document
					.Element(el =>
					         !string.IsNullOrWhiteSpace(el.Text)
					         && el.Text.Contains(PageStrings.ExpenseClaimPage_ExpectedSaveText))
					.Text;
			}
		}

		Div PreviewPrintout
		{
			get { return Document.Div(Find.ByName("b$printpreview")); }
		}

		public ExpenseVoucherPage SaveSupportingDocuments(Stream targetStream)
		{
			PreviewPrintout.Click(); //Clicking this button will open a new window and a print dialog
			// https://economy.jayway.com/Agresso/System/printpreview.aspx
			// ?instanceid=92e61093969b42c1jjj49b3dbc960&framename=TTT002&client=DS&interface=Agresso.Interface.Travel.ITravelExpense&assembly=Agresso.Interface.Travel
			Browser popup = Browser.AttachTo<IE>(Find.ByUrl(new Regex(".+printpreview.aspx"))); //Match url ending in "_CoverPage.aspx"

			using (var writer = new StreamWriter(targetStream, Encoding.UTF8))
			{
				writer.Write(popup.ActiveElement.Parent.OuterHtml);
				writer.Flush();
			}

			var page = popup.Page<ExpenseVoucherPage>();
			page.SetBrowser(popup);
			return page;
		}

		#endregion
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