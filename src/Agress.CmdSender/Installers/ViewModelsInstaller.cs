using Agress.CmdSender.Modules.Commands.EditCommand;
using Caliburn.Micro;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NLog;
using LogManager = NLog.LogManager;

namespace Agress.CmdSender.Installers
{
	public class ViewModelsInstaller
		: IWindsorInstaller
	{
		static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			_logger.Debug("installing window manager and event aggregator");

			container.Register(
				Component.For<IWindowManager>()
					.ImplementedBy<WindowManager>()
					.LifeStyle.Singleton,
				Component.For<IEventAggregator>()
					.ImplementedBy<EventAggregator>()
					.LifeStyle.Singleton,
				AllTypes.FromThisAssembly().InSameNamespaceAs<EditCommandViewModel>()
					.LifestyleTransient()
					.WithServiceSelf());
		}
	}
}