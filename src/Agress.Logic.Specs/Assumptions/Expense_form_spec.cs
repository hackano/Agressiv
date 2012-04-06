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
using System.IO;
using System.Linq;
using System.Text;
using Agress.Logic.Pages;
using Machine.Specifications;
using Agress.Logic.Framework;
using WatiN.Core;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

namespace Agress.Logic.Specs.Assumptions
{
	static class SampleNavigations
	{
		public static void FillOutFirst(ExpenseClaimPage p, Browser b)
		{
			// assumes: at initial expenses page.
			b.PressTab();
			b.PressTab(); // press tab twice, to get to expense types

			p.ExpenseType.Select(PageStrings.ExpenseClaimPage_TypeOfExpense_Expense);
			p.Cause.Value = "SampleNavigations: Speaking in Berlin";
			p.Comment.Value = "SampleNavigations: Sample navigation from first page.";
		}

		public static void FillOutThird(ExpenseClaimPage page)
		{
			page.AddRow("DATORTILL", DateTime.UtcNow, "Datortillbehörsutlägg", 1.0);
			page.DoneWithRows();
		}
	}

	[Subject("Expense Claim Page")]
	public class when_navigating_to_expense_claim_page
		: logged_in_context
	{
		static ExpenseClaimPage page;

		Because of = () =>
			page = browser.GoToPage<ExpenseClaimPage>(AgressoNamesAndIds.ContainerFrameId);

		It should_contain_expected_text = () =>
			page.Document.ContainsText(PageStrings.ExpenseClaimPage_ExpectedString).ShouldBeTrue();

		It should_contain_name_of_person = () =>
			page.Document.ContainsText(creds.UserName).ShouldBeTrue();
	}

	[Subject("Expense Claim Form (1st page)")]
	public class when_filling_out_first_page
		: logged_in_context
	{
		static ExpenseClaimPage page;

		Establish context = () =>
			{
				page = browser.GoToPage<ExpenseClaimPage>(AgressoNamesAndIds.ContainerFrameId);
				SampleNavigations.FillOutFirst(page, browser);
			};

		Because of = () => 
			page.Next1.Click();

		It should_move_to_third_tab_expenses = () =>
			page.ActiveTab.Text.ShouldEqual(PageStrings.ExpenseClaimPage_ExpenseTab);
	}

	[Subject("Expense Claim Form (3rd page)")]
	public class when_filling_out_expense_table
		: logged_in_context
	{
		const string ExpenseDescription = "Utlägg datortillbehör";
		static ExpenseClaimPage page;
		static ExpenseClaimPage.ExpenseRow subjectRow;

		Establish context = () =>
			{
				page = browser.GoToPage<ExpenseClaimPage>(AgressoNamesAndIds.ContainerFrameId);
				SampleNavigations.FillOutFirst(page, browser);
				page.Next1.Click();
			};

		Because of = () =>
			{
				page.AddRow("DATORTILL", DateTime.UtcNow, ExpenseDescription, 567.0);
				page.DoneWithRows();
				subjectRow = page.ExpenseTable.First();
			};

		It should_contain_description = () =>
			subjectRow.Description.ShouldEqual(ExpenseDescription);

		It should_contain_amount = () =>
			subjectRow.Amount.ShouldEqual(567.0);

		It should_have_row = () =>
			subjectRow.Getter().ShouldNotBeNull();
	}


	[Subject("Expense Claim Form (4th page)")]
	public class when_clicking_adding_expense_line
		: logged_in_context
	{
		static ExpenseClaimPage page;

		Establish context = () =>
			{
				page = browser.GoToPage<ExpenseClaimPage>(AgressoNamesAndIds.ContainerFrameId);
				SampleNavigations.FillOutFirst(page, browser);
				page.Next1.Click();
				SampleNavigations.FillOutThird(page);
				page.Next1.Click();
			};

		Because of = () => page.SubmitDraft();

		It should_display_saved_text = () =>
			page.InfoBlock.ShouldContain(PageStrings.ExpenseClaimPage_ExpectedSaveText);
	}

	[Subject("Expense Claim Form (when done)")]
	public class when_done_can_print
		: logged_in_context
	{
		static string printedText;
		static ExpenseClaimPage page;

		Establish context = () =>
		{
			page = browser.GoToPage<ExpenseClaimPage>(AgressoNamesAndIds.ContainerFrameId);
			SampleNavigations.FillOutFirst(page, browser);
			page.Next1.Click();
			SampleNavigations.FillOutThird(page);
			page.Next1.Click();
			page.SubmitDraft();
		};

		Because of = () =>
			{
				using (var targetStream = new MemoryStream())
				{
					page.SaveSupportingDocuments(targetStream);
					printedText = Encoding.UTF8.GetString(targetStream.ToArray());
				}
			};

		It should_have_saved_pdf = () =>
			printedText.ShouldContain(PageStrings.ExpenseClaimPrintOut_ExpectedHeading);
	}
}