using System;
using Agress.Logic;
using MassTransit;

namespace Agress.WebDriver
{
	internal class Program
	{
		private IServiceBus _Bus;

		private static void Main(string[] args)
		{
			var p = new Program();
			p.Run();
			Console.WriteLine("Key to exit...");
			Console.ReadKey(true);
		}

		private void Run()
		{
			_Bus = ServiceBusFactory.New(sbc =>
			{
			    sbc.ReceiveFrom("rabbitmq://localhost/Agressiv");
				sbc.Subscribe(s =>
				              	{
				              		var sett = Settings.Default;
				              		s.Consumer(() => new MainPresenter(
										sett.Login_Username,
										sett.Login_Password,
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