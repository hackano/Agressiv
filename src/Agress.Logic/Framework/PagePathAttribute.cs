using System;
using Magnum.Reflection;
using MassTransit.Util;
using WatiN.Core;
using System.Linq;
using Magnum.Extensions;

namespace Agress.Logic.Framework
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class PagePathAttribute
		: Attribute, Driver
	{
		readonly Type[] _extraDrivers;
		readonly Uri _url;

		public PagePathAttribute([NotNull] string path, 
			params Type[] afterwards)
		{
			if (path == null) throw new ArgumentNullException("path");

			if (afterwards != null && afterwards.Length > 0 && afterwards.Any(d => !d.Implements(typeof(Driver))))
				throw new ArgumentException("Not all of extra drivers implement the Driver interface.");

			_extraDrivers = afterwards ?? new Type[0];
			_url = ComputeUri(BaseUri.Uri, path);
		}

		internal static Uri ComputeUri(Uri baseUri, string path)
		{
			var queryStart = path.IndexOf('?');
			var hasQuery = queryStart != -1;
			var lenPath = hasQuery ? queryStart : path.Length;

			var builder = new UriBuilder(baseUri)
				{
					Path = path.Substring(0, lenPath),
				};

			if (hasQuery)
				builder.Query = path.Substring(queryStart + 1, path.Length - queryStart - 1);

			return builder.Uri;
		}

		/// <summary>
		/// Gets the page uri
		/// </summary>
		public Uri Url
		{
			get { return _url; }
		}

		public void Drive(Browser b)
		{
			b.GoTo(Url);

			foreach (var driver in _extraDrivers.Select(t => FastActivator.Create(t)).Cast<Driver>())
				driver.Drive(b);
		}
	}
}