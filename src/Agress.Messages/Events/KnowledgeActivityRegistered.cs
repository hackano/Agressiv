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

namespace Agress.Messages.Events
{
	public interface KnowledgeActivityRegistered
	{
		/// <summary>
		/// As specified by the system as the voucher was registered.
		/// </summary>
		string VoucherNumber { get; }

		/// <summary>
		/// Html page of the voucher
		/// </summary>
		byte[] Voucher { get; }

		/// <summary>The UserName of the person who is doing the registering.</summary>
		string UserName { get; }

		/// <summary>
		/// Gets the Uri of the scanned image.
		/// </summary>
		Uri Scan { get; }
	}
}