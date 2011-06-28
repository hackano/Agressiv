using System;
using Agress.Core.Events;
using MassTransit;
using log4net.Config;

namespace Agress.ExcelCreator
{
	class Program
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
			BasicConfigurator.Configure();

			_Bus = ServiceBusFactory.New(sbc =>
			{
				sbc.ReceiveFrom("rabbitmq://localhost/Agress.ExcelCreator");
				sbc.Subscribe(s => s.Consumer(() => new Creator(_Bus)).Permanent());
				sbc.UseRabbitMq();
				sbc.UseRabbitMqRouting();
			});
		}
	}

	internal class Creator : Consumes<SingleDayTimeReported>.All, Consumes<FullWeekReported>.All
	{
		private readonly IServiceBus _Bus;

		public Creator(IServiceBus bus)
		{
			if (bus == null) throw new ArgumentNullException("bus");
			_Bus = bus;
		}

		public void Consume(SingleDayTimeReported message)
		{
			Console.WriteLine("got single day-message!");
		}

		public void Consume(FullWeekReported message)
		{
			Console.WriteLine("got full week message!!");
		}
	}
}
