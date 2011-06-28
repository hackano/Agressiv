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