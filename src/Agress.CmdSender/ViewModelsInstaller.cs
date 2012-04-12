using Caliburn.Micro;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Agress.CmdSender
{
	public class ViewModelsInstaller
		: IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Component.For<IWindowManager>()
					.ImplementedBy<WindowManager>()
					.LifeStyle.Singleton,
				Component.For<IEventAggregator>()
					.ImplementedBy<EventAggregator>()
					.LifeStyle.Singleton,
				AllTypes.FromThisAssembly().Pick());
		}
	}
}