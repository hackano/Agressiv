using System.Windows;

namespace Agress.CmdSender.Modules.Commands.EditCommand
{
	/// <summary>
	/// Interaction logic for EditCommandView.xaml
	/// </summary>
	public partial class EditCommandView : Window
	{
		public EditCommandView()
		{
			InitializeComponent();
			
		}
		private void WindowClosed(object sender, System.EventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}