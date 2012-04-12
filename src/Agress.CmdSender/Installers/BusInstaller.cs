using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MassTransit;
using MassTransit.NLogIntegration;
using NLog;

namespace Agress.CmdSender.Installers
{
	public class BusInstaller : IWindsorInstaller
	{
		static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		readonly string _endpointUri;

		public BusInstaller(string endpointUri)
		{
			_endpointUri = endpointUri;
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Component.For<IServiceBus>()
					.UsingFactoryMethod(() => ServiceBusFactory.New(sbc =>
						{
							_logger.Debug("creating service bus");
							sbc.UseNLog();
							sbc.ReceiveFrom(_endpointUri);
							sbc.UseRabbitMqRouting();
						})));
		}
	}
}