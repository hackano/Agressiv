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

using System.Globalization;
using NUnit.Framework;

namespace Agress.Logic.Specs
{
	public class FormattingDoubleTests
	{
		[Test]
		public void Format_to_double_zeroes()
		{
			Assert.That(
				567.556.ToString("0.00", new CultureInfo("en-US")),
				Is.EqualTo("567.56"));
		}
	}
}