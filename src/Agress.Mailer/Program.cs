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
using MassTransit;
using MassTransit.Log4NetIntegration;
using Topshelf;
using log4net.Config;

namespace Agress.Mailer
{
	internal class Program
	{
		IServiceBus _bus;

		static void Main(string[] args)
		{
			BasicConfigurator.Configure();

			Thread.CurrentThread.Name = "Mailer Main Thread";

			HostFactory.Run(x =>
				{
					x.Service<Program>(s =>
						{
							s.ConstructUsing(name => new Program());
							s.WhenStarted(p => p.Start());
							s.WhenStopped(p => p.Stop());
							s.WhenContinued(p => p.Start());
							s.WhenPaused(p => p.Stop());
						});

					x.RunAsNetworkService();

					x.SetDescription("Sends e-mails based on the events from reported expenses.");
					x.SetDisplayName("Agressiv.Mailer");
					x.SetServiceName("Agressiv.Mailer");
				});
		}

		void Start()
		{
			_bus = ServiceBusFactory.New(sbc =>
				{
					sbc.UseLog4Net();
					sbc.UseRabbitMqRouting();
					sbc.ReceiveFrom(string.Format("rabbitmq://localhost/{0}", typeof (Program).Namespace));
					sbc.Subscribe(s => 
						s.Consumer(() => new MailSender(new MailClientImpl(), new SystemProcessManager()))
							.Permanent());
				});
		}

		void Stop()
		{
			if (_bus != null)
				_bus.Dispose();
		}
	}
}