using System;
using System.Windows.Forms;
using MassTransit;

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
			_Bus = ServiceBusFactory.New(sbc =>
			{
				sbc.ReceiveFrom("rabbitmq://localhost/Agressiv");
				sbc.UseRabbitMq();
				sbc.UseRabbitMqRouting();
			});

			Application.Run(new MainForm(_Bus));
		}
	}
}