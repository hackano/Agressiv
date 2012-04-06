using Agress.Logic.Framework;
using Agress.Logic.Pages;
using Agress.Logic.Specs.Framework;
using Machine.Specifications;
using WatiN.Core;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

namespace Agress.Logic.Specs.Assumptions
{
	public class logged_in_context
	{
		protected static Credentials creds;
		protected static Browser browser;

		Establish context = () =>
			{
				browser = new IE();
				var page = browser.GoToPage<LoginPage>();
				creds = TestFactory.GetCredentialsFromFileOrEnv();
				page.LogIn(creds);
			};

		Cleanup closing = () =>
			{
				if (browser == null)
					return;
				
				browser.Frame(AgressoNamesAndIds.MenuFrameId)
					.Page<LeftMenu>()
					.LogOut();

				browser.Close();
				browser.Dispose();
			};
	}
}