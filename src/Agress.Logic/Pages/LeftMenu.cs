using WatiN.Core;

namespace Agress.Logic.Pages
{
	public class LeftMenu
		: Page
	{
		public Image PlusLink
		{
			get { return Document.Image(Find.BySrc(s => s.EndsWith("Plus.gif"))); }
		}

		public Link TimeAndExpenses
		{
			get { return Document.Link(Find.ByText(PageStrings.LeftMenuPage_TimeAndExpenses)); }
		}

		public Link ExpenseClaim
		{
			get { return Document.Link(Find.ByText(PageStrings.LeftMenuPage_ExpenseClaim)); }
		}

		public void LogOut()
		{
			if (!Document.Elements.Exists(Find.ByName("menuButtons$_logout")))
				return;

			Document.Element(Find.ByName("menuButtons$_logout"))
				.ClickNoWait();
		}
	}
}
