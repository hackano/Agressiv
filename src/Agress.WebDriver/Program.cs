using System;
using System.Threading;
using Agress.Logic;
using MassTransit;
using MassTransit.NLogIntegration;

namespace Agress.WebDriver
{
	internal class Program
	{
		private IServiceBus _bus;

		[STAThread]
		private static void Main(string[] args)
		{
			Thread.CurrentThread.SetApartmentState(ApartmentState.STA);

			var p = new Program();
			
			p.Run();

			Console.WriteLine("Key to exit...");
			Console.ReadKey(true);

			p.Stop();
		}

		private void Stop()
		{
			if (_bus != null)
				_bus.Dispose();
		}

		private void Run()
		{
			_bus = ServiceBusFactory.New(sbc =>
			{
				sbc.UseNLog();

				sbc.ReceiveFrom("rabbitmq://localhost/Agressiv.WebDriver");

				sbc.Subscribe(s =>
				{
					var sett = Settings.Default;
					var pwd = sett.Login_Password;
					s.Consumer(() =>
						new MainPresenter(_bus,
							sett.Login_Username,
							string.IsNullOrEmpty(pwd) ? Password.String : pwd,
							sett.Login_Client,
							sett.Login_Url
						));
				});

				sbc.UseRabbitMqRouting();
			});
		}
	}
}