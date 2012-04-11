using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Agress.CmdSender.Messages;
using Agress.CmdSender.Modules.EditCommand;
using Agress.Messages.Commands;
using MassTransit;
using MassTransit.NLogIntegration;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Agress.CmdSender
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		readonly Dictionary<Type, object> _defaultFor = new Dictionary<Type, object>
			{
				{typeof (RegisterKnowledgeActivityExpense), new RKAE()}
			};

		readonly EditCommandViewModel model;


		public MainWindow()
		{
			InitializeComponent();

			IServiceBus sb = ServiceBusFactory.New(sbc =>
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

			Type t = Type.GetType(CommandTypes.SelectedValue.ToString());
			object defObj = _defaultFor[t];
			model.CmdEditor = JsonConvert.SerializeObject(defObj);
		}

		private void btnGetFile_Click(object sender, RoutedEventArgs e)
		{
			var dlg = new OpenFileDialog {Filter = "Image Files (*.png, *.jpg)|*.png;*.jpg"};
			bool? result = dlg.ShowDialog();

			if (result == true)
			{
				model.ImageFile = dlg.FileName;
			}
		}
	}
}