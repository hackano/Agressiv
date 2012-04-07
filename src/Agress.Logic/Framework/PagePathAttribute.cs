using System;
using MassTransit.Util;
using WatiN.Core;

namespace Agress.Logic.Framework
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class PagePathAttribute
		: Attribute, Driver
	{
		readonly Uri _url;

		public PagePathAttribute([NotNull] string path)
		{
			if (path == null) throw new ArgumentNullException("path");

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
			if (b.Frames.Exists(Find.ById(AgressoNamesAndIds.ContainerFrameId))
				&& b.Frame(AgressoNamesAndIds.ContainerFrameId).ContainsText(
					PageStrings.SessionExpiryText))
				b.Button("button").Click(); // lol

			b.GoTo(Url);
		}
	}
}