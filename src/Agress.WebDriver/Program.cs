using System;
using System.Threading;
using Agress.Logic;
using MassTransit;
using log4net.Config;

namespace Agress.WebDriver
{
	internal class Program
	{
		private IServiceBus _Bus;

		[STAThread]
		private static void Main(string[] args)
		{
			Thread.CurrentThread.SetApartmentState(ApartmentState.STA);

			var p = new Program();
			p.Run();
			Console.WriteLine("Key to exit...");
			Console.ReadKey(true);
		}

		private void Run()
		{
			BasicConfigurator.Configure();

			_Bus = ServiceBusFactory.New(sbc =>
			{
				sbc.ReceiveFrom("rabbitmq://localhost/Agressiv.WebDriver");
				sbc.Subscribe(s =>
				{
					var sett = Settings.Default;
					var pwd = sett.Login_Password;
					s.Consumer(() =>
						new MainPresenter(_Bus,
						sett.Login_Username,
						string.IsNullOrEmpty(pwd) ? Password.String : pwd,
						sett.Login_Client,
						sett.Login_Url
						));
				});

				sbc.UseRabbitMq();
				sbc.UseRabbitMqRouting();
			});
		}
	}
}