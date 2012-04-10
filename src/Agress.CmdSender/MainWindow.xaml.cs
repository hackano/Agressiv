using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Agress.CmdSender.Messages;
using Agress.CmdSender.Modules.EditCommand;
using Agress.Messages.Commands;
using Magnum.Reflection;
using MassTransit;
using MassTransit.NLogIntegration;
using Newtonsoft.Json;

namespace Agress.CmdSender
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		readonly EditCommandViewModel model;

		readonly Dictionary<Type, object> _defaultFor = new Dictionary<Type, object>
			{
				{ typeof(RegisterKnowledgeActivityExpense), new RKAE() }
			};


		public MainWindow()
		{
			InitializeComponent();

			var sb = ServiceBusFactory.New(sbc =>
				{
					sbc.UseNLog();
					sbc.ReceiveFrom("rabbitmq://localhost/Agress.CmdSender");
					sbc.UseRabbitMqRouting();
				});

			model = new EditCommandViewModel(sb);
			DataContext = model;
			lbHeaders.ItemsSource = model.Headers;
			CommandTypes.ItemsSource = model.CmdTypes;
			CommandTypes.DisplayMemberPath = "Value";
			CommandTypes.SelectedValuePath = "Key";
		}

		private void CommandTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (CommandTypes.SelectedIndex == -1)
				return;

			var t = Type.GetType(CommandTypes.SelectedValue.ToString());
			var defObj = _defaultFor[t];
			model.CmdEditor = JsonConvert.SerializeObject(defObj);
		}

	
	}
}
