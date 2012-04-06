using System;
using MassTransit.Util;

namespace Agress.Logic.Pages
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class PagePathAttribute
		: Attribute
	{
		readonly Uri _url;

		public PagePathAttribute([NotNull] string path)
		{
			if (path == null) throw new ArgumentNullException("path");
			var builder = new UriBuilder(BaseUri.Uri)
				{
					Path = path
				};
			_url = builder.Uri;
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