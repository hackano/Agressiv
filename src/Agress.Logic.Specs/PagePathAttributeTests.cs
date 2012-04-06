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
using Agress.Logic.Framework;
using NUnit.Framework;

namespace Agress.Logic.Specs
{
	public class PagePathAttributeTests
	{
		[Test]
		public void CanExtractQuery()
		{
			var bUri = new Uri("http://a.com/");
			var p0 = "/b?c=3";
			var p1 = PagePathAttribute.ComputeUri(bUri, p0);

			Assert.That(p1, Is.EqualTo(new Uri("http://a.com/b?c=3")));
		}
		[Test]
		public void CanComputeWithoutQuery()
		{
			var bUri = new Uri("http://a.com/");
			var p0 = "/b";
			var p1 = PagePathAttribute.ComputeUri(bUri, p0);

			Assert.That(p1, Is.EqualTo(new Uri("http://a.com/b")));
		}
	}
}