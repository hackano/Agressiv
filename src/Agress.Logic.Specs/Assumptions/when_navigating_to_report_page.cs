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

using Agress.Logic.Framework;
using Agress.Logic.Pages;
using Machine.Specifications;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

namespace Agress.Logic.Specs.Assumptions
{
	[Subject("Travel Claim Page")]
	public class when_navigating_to_report_page
		: logged_in_context
	{
		static TravelClaimPage page;

		Because of = () =>
			page = browser.GoToPage<TravelClaimPage>(AgressoNamesAndIds.ContainerFrameId);
	
		It should_contain_expected_text = () => 
			page.Document.ContainsText(PageStrings.TravelClaimPage_ExpectedString).ShouldBeTrue();

		It should_contain_name_of_person = () =>
			page.Document.ContainsText(creds.UserName).ShouldBeTrue();
	}
}