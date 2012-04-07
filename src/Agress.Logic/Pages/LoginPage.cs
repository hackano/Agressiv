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
using System.Threading;
using Agress.Logic.Framework;
using WatiN.Core;

namespace Agress.Logic.Pages
{
	[PagePath("/agresso/System/Login.aspx")]
	public class LoginPage
		: Page
	{
		public TextField UserName
		{
			get { return Document.TextField("_name"); }
		}

		public TextField Password
		{
			get { return Document.TextField("_password"); }
		}

		public TextField Client
		{
			get { return Document.TextField("_client"); }
		}

		public Button Submit
		{
			get { return Document.Button("Button1"); }
		}

		private bool HasContainerFrame()
		{
			return ProtectedAgainstUninitializedPages(() => 
				Document.Element(Find.ById(AgressoNamesAndIds.ContainerFrameId)).Exists);
		}

		private bool OnFirstPage()
		{
			SpinWait.SpinUntil(HasContainerFrame);
			return ProtectedAgainstUninitializedPages(() => 
				Document.Frame(AgressoNamesAndIds.ContainerFrameId)
					.TableCell(Find.ByText(PageStrings.TimeReportPage_ExpectedTitle))
					.Exists);
		}

		private static T ProtectedAgainstUninitializedPages<T>(Func<T> action, T def = default(T))
		{
			try
			{
				return action();
			}
			catch (UnauthorizedAccessException)
			{
				return def;
			}
		}

		public void LogIn(Credentials creds)
		{
			if (Document.Elements.Exists(Find.ByName("menuButtons$_logout"))
				|| Document.Frames.Exists(Find.ById(AgressoNamesAndIds.MenuFrameId)))
				return;

			UserName.Value = creds.UserName;
			Password.Value = creds.Password;
			Client.Value = AgressoNamesAndIds.AlwaysThisClient;
			Submit.ClickNoWait();

			SpinWait.SpinUntil(OnFirstPage);
		}
	}
}