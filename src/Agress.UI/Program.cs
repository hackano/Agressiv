using System;
using System.Windows.Forms;
using MassTransit;
using log4net.Config;

namespace Agress.UI
{
	internal class Program
	{
		private IServiceBus _Bus;

		/// <summary>
		/// 	The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var p = new Program();
			p.Run();
		}

		private void Run()
		{
			BasicConfigurator.Configure();

			_Bus = ServiceBusFactory.New(sbc =>
			{
				sbc.ReceiveFrom("rabbitmq://localhost/Agressiv.UI");
				sbc.UseRabbitMq();
				sbc.UseRabbitMqRouting();
			});

			Application.Run(new MainForm(_Bus));
		}
	}
}