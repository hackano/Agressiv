using WatiN.Core;

namespace Agress.Logic.Framework
{
	public static class ElementExtensions
	{
		public static T FindParent<T>(this Element child)
			where T : Element
		{
			var parent = child;
			while ((parent = parent.Parent).GetType() != typeof(T))
				;
			return (T)parent;
		}

	}
}