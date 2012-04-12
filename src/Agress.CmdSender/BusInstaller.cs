using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MassTransit;
using MassTransit.NLogIntegration;

namespace Agress.CmdSender
{
	public class BusInstaller : IWindsorInstaller
	{
		readonly string _endpointUri;

		public BusInstaller(string endpointUri)
		{
			_endpointUri = endpointUri;
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<IServiceBus>()
			                   	.UsingFactoryMethod(() => ServiceBusFactory.New(sbc =>
			                   		{
			                   			sbc.UseNLog();
			                   			sbc.ReceiveFrom(_endpointUri);
			                   			sbc.UseRabbitMqRouting();
			                   		})));
		}
	}
}