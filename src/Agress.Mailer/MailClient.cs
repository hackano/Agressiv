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
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Linq;

namespace Agress.Mailer
{
	public interface MailClient
	{
		void Send(MailMessage message, string username);
	}

	public class MailClientImpl
		: MailClient
	{
		readonly SmtpClient _client;

		public MailClientImpl()
		{
			// http://stackoverflow.com/questions/7244589/smtpclient-and-app-config-system-net-configuration
			_client = new SmtpClient();
		}

		public void Send(MailMessage message, string username)
		{
			ConfigureMessage(message, username);
			SendWithClient(message);
		}

		/// <summary>Called after configure message.</summary>
		protected virtual void SendWithClient(MailMessage message)
		{
			// consider minimum 5 KB/s sending
			const int msPerSecond = 1000;
			var msgSize = message.Attachments.Sum(a => (double)a.ContentStream.Length) / 1024.0;
			_client.Timeout = Convert.ToInt32(Math.Round(msgSize*msPerSecond));

			_client.Send(message);
		}

		/// <summary>Configure the message properties</summary>
		protected virtual void ConfigureMessage(MailMessage message, string username)
		{
			message.To.Add(GetToAddress());
			message.From = GetFromAddress(username);
			message.BodyEncoding = Encoding.UTF8;
			message.SubjectEncoding = Encoding.UTF8;
		}

		static MailAddress GetToAddress()
		{
			var mail = ConfigurationManager.AppSettings["email_to"];
			var name = ConfigurationManager.AppSettings["email_to_name"];
			return new MailAddress(mail, name);
		}

		static MailAddress GetFromAddress(string username)
		{
			var mail = ConfigurationManager.AppSettings["email_from"];
			var name = ConfigurationManager.AppSettings["email_from_name"];
			return new MailAddress(mail, string.Format(name, username));
		}
	}
}