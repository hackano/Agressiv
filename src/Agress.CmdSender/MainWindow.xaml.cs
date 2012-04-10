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
using Agress.CmdSender.Modules.EditCommand;
using MassTransit;
using MassTransit.NLogIntegration;

namespace Agress.CmdSender
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			var sb = ServiceBusFactory.New(sbc =>
				{
					sbc.UseNLog();
					sbc.ReceiveFrom("rabbitmq://localhost/Agress.CmdSender");
					sbc.UseRabbitMqRouting();
				});

			var model = new EditCommandViewModel(sb);
			lbHeaders.ItemsSource = model.Headers;
		}

	
	}
}
