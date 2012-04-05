using System;
using MassTransit.Util;

namespace Agress.Logic.Pages
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class PagePathAttribute
		: Attribute
	{
		readonly Uri _url;

		public PagePathAttribute([NotNull] string url)
		{
			if (url == null) throw new ArgumentNullException("url");
			_url = new Uri(url);
		}

		/// <summary>
		/// Gets the page uri
		/// </summary>
		public Uri Url
		{
			get { return _url; }
		}
	}
}