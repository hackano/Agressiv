using System;
using Magnum.Reflection;
using MassTransit.Util;
using WatiN.Core;
using Magnum.Extensions;

namespace Agress.Logic.Framework
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class PageFromDriverAttribute
		: Attribute, Driver
	{
		readonly Type _driver;

		public PageFromDriverAttribute([NotNull] Type driver)
		{
			if (driver == null) throw new ArgumentNullException("driver");
			if (!driver.Implements<Driver>())
				throw new ArgumentException(string.Format("Type {0} must implement Driver interface.", driver), 
					"driver");

			_driver = driver;
		}

		public void Drive(Browser b)
		{
			var driver = (Driver)FastActivator.Create(_driver);
			driver.Drive(b);
		}
	}
}