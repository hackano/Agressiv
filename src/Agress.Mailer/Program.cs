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

using System.Threading;
using Topshelf;

namespace Agress.Mailer
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Thread.CurrentThread.Name = "Mailer Main Thread";

			HostFactory.Run(x =>
				{
					x.Service<Program>(s =>
						{
							s.ConstructUsing(name => new Program());
							s.WhenStarted(p => p.Start());
							s.WhenStopped(p => p.Stop());
						});

					x.RunAsNetworkService();

					x.SetDescription("Handles the domain logic for the Documently Application.");
					x.SetDisplayName("Documently Domain Service");
					x.SetServiceName("Documently.Domain.Service");
				});
		}

		void Start()
		{
		
		}

		void Stop()
		{
		}
	}
}