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

namespace Agress.Logic
{
	public interface Credentials
	{
		string UserName { get; }
		string Password { get; }
	}

	/// <summary>
	/// Gets the credentials for the login procedure.
	/// </summary>
	public sealed class EnvironmentCredentials : Credentials
	{
		readonly string _username;
		readonly string _password;

		public EnvironmentCredentials()
		{
		}

		public EnvironmentCredentials(string username, string password)
		{
			_username = username;
			_password = password;
		}

		public string UserName
		{
			get { return ENV("AGRESSO_USERNAME") ?? _username; }
		}

		public string Password
		{
			get { return ENV("AGRESSO_PASSWORD") ?? _password; }
		}

		private static string ENV(string envKey)
		{
			return Environment.GetEnvironmentVariable(envKey);
		}
	}
}