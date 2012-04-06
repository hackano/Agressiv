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
using WatiN.Core;
using System.Linq;
using TimeoutException = WatiN.Core.Exceptions.TimeoutException;

namespace Agress.Logic.Framework
{
	public static class BrowserExtensions
	{
		public static T GoToPage<T>(this Browser b, string frame = null)
			where T : Page, new()
		{
			var attr = GetPageDriver<T>();
			
			AssertNonNull<T>(attr);

			try { b.WaitForComplete(2); } catch (TimeoutException) {}
			attr.Drive(b);

			return frame == null ? b.Page<T>() : b.Frame(frame).Page<T>();
		}

		static void AssertNonNull<T>(Driver attr) where T : Page, new()
		{
			if (attr == null)
				throw new ArgumentException(string.Format("Argument generic type {0}, ", typeof (T).Name) +
				                            "doesn't have an attribute '" +
				                            typeof (PagePathAttribute).Name + "', or '" +
				                            typeof(PageFromDriverAttribute).Name + "' specified!");
		}

		static Driver GetPageDriver<T>()
		{
			var pp = typeof (T).GetCustomAttributes(typeof (PagePathAttribute), true).FirstOrDefault() as PagePathAttribute;
			
			return pp ?? GetDriver<T>();
		}

		static Driver GetDriver<T>()
		{
			return typeof (T).GetCustomAttributes(typeof (PageFromDriverAttribute), true).FirstOrDefault() as Driver;
		}
	}
}