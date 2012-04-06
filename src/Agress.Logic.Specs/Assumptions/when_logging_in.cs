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
using Agress.Logic.Specs.Framework;
using Machine.Specifications;
using WatiN.Core;

// ReSharper disable InconsistentNaming

namespace Agress.Logic.Specs.Assumptions
{
	[Subject("Login Page")]
	public class when_logging_in
	{
		Establish context = () =>
			{
				browser = new IE();
				page = browser.GoToPage<LoginPage>();
			};

		static LoginPage page;
		static Browser browser;

		Because of = () =>
			page.LogIn(TestFactory.GetCredentialsFromFileOrEnv());

		It should_navigate_to_the_default_page = () => 
			browser.Url.ShouldEndWith("Agresso/Default.aspx?type=topgen&menu_id=TS294");
	}
}