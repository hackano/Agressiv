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

namespace Agress.Messages.Commands
{
	/// <summary>
	/// Computer accessories or software that we use at work.
	/// </summary>
	public interface RegisterAccessoryExpense
	{
		/// <summary>
		/// Why did you have this expense? What was it for?
		/// </summary>
		string Cause { get; }
		
		/// <summary>
		/// Expense description and otherwise clarifying comment.
		/// </summary>
		string Comment { get; }

		/// <summary>
		/// The cost including value added tax, of the expense.
		/// </summary>
		double Amount { get; }
	}

	public interface RegisterKnowledgeActivityExpense
	{
		/// <summary>
		/// Why did you have this expense? What was it for?
		/// </summary>
		string Cause { get; }

		/// <summary>
		/// Expense description and otherwise clarifying comment.
		/// </summary>
		string Comment { get; }

		/// <summary>
		/// The cost including value added tax, of the expense.
		/// </summary>
		double Amount { get; }
	}
}