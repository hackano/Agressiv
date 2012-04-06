// Copyright 2012 Henrik Feldt
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.

using System;
using Agress.Logic.Pages;
using WatiN.Core;
using System.Linq;

namespace Agress.Logic.Framework
{
	public static class BrowserExtensions
	{
		public static T GoToPage<T>(this Browser b)
			where T : Page, new()
		{
			var attr = typeof(T)
				.GetCustomAttributes(typeof(PagePathAttribute), true)
				.FirstOrDefault()
				as PagePathAttribute;

			if (attr == null)
				throw new ArgumentException(string.Format("Argument generic type {0}, ", typeof(T).Name) +
				                            "doesn't have an attribute '" +
				                            typeof(PagePathAttribute).Name + "' specified.");

			b.GoTo(attr.Url);

			return b.Page<T>();
		}
	}
}