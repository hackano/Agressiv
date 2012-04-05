using System;
using Agress.Messages.Events;
using MassTransit;
using MassTransit.NLogIntegration;

namespace Agress.ExcelCreator
{
	class Program
	{
		private IServiceBus _bus;

		private static void Main(string[] args)
		{
			var p = new Program();
			p.Run();
			Console.WriteLine("Key to exit...");
			Console.ReadKey(true);
		}

		private void Run()
		{
			_bus = ServiceBusFactory.New(sbc =>
				{
					sbc.UseNLog();
					sbc.ReceiveFrom("rabbitmq://localhost/Agress.ExcelCreator");
					sbc.Subscribe(s => s.Consumer(() => new Creator(_bus)).Permanent());
					sbc.UseRabbitMqRouting();
				});
		}
	}

	internal class Creator
		: Consumes<SingleDayTimeReported>.All,
		  Consumes<FullWeekReported>.All
	{
		private readonly IServiceBus _bus;

		public Creator(IServiceBus bus)
		{
			if (bus == null) throw new ArgumentNullException("bus");
			_bus = bus;
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
