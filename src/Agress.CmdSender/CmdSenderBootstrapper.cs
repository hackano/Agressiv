using System;
using System.Collections.Generic;
using Agress.CmdSender.Installers;
using Agress.CmdSender.Modules.Commands.EditCommand;
using Caliburn.Micro;
using Castle.Windsor;

namespace Agress.CmdSender
{
	public class CmdSenderBootstrapper : Bootstrapper<EditCommandViewModel>
	{
		private IWindsorContainer _container;

		protected override void Configure()
		{
			_container = new WindsorContainer();
			_container.Install(
				new BusInstaller("rabbitmq://localhost/Agress.CmdSender"),
				new ViewModelsInstaller(),
				new CredentialsInstaller());
		}

		protected override object GetInstance(Type service, string key)
		{
			return string.IsNullOrWhiteSpace(key)
				? _container.Resolve(service)
				: _container.Resolve(key, service);
		}

		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			return (IEnumerable<object>)_container.ResolveAll(service);
		}

		protected override void BuildUp(object instance)
		{
			_container.BuildUp(instance);
		}

		protected override void OnExit(object sender, EventArgs e)
		{
			_container.Dispose();
		}
	}
}