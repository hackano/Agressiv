using System.Linq;
using Castle.Windsor;

namespace Agress.CmdSender
{
	public static class WindsorExtensions
	{
		public static void BuildUp(this IWindsorContainer container, object instance)
		{
			instance.GetType().GetProperties()
				.Where(property => property.CanWrite && property.PropertyType.IsPublic)
				.Where(property => container.Kernel.HasComponent(property.PropertyType))
				.ToList()
				.ForEach(property => property.SetValue(instance, container.Resolve(property.PropertyType), null));
		}
	}
}