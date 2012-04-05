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

using System.IO;
using Agress.Logic.Pages;
using Agress.Logic.Specs.Framework;
using Machine.Specifications;
using WatiN.Core;

// ReSharper disable InconsistentNaming

namespace Agress.Logic.Specs
{
	internal static class TestFactory
	{
		public static Credentials GetCredentialsFromFileOrEnv()
		{
			if (File.Exists("credentials.txt"))
			{
				var rows = File.ReadAllLines("credentials.txt");
				return new EnvironmentCredentials(rows[0], rows[1]);
			}
			return new EnvironmentCredentials();
		}
	}

	public class when_logging_in
	{
		Establish context = () =>
			{
				var browser = new IE();
				page = browser.GoToPage<LoginPage>();
			};

		static LoginPage page;

		Because of = () => page.LogIn(TestFactory.GetCredentialsFromFileOrEnv());


	}
}