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
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using Agress.Messages.Events;
using Agress.Messages.Mailer;
using MassTransit;
using MassTransit.Util;
using NLog;

namespace Agress.Mailer
{
	public class MailSender
		: Consumes<KnowledgeActivityRegistered>.Context
	{
		static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		readonly MailClient _sender;
		readonly ProcessManager _processManager;

		public MailSender(
			[NotNull] MailClient sender,
			[NotNull] ProcessManager processManager)
		{
			if (sender == null) throw new ArgumentNullException("sender");
			if (processManager == null) throw new ArgumentNullException("processManager");
			_sender = sender;
			_processManager = processManager;
		}

		void Consumes<IConsumeContext<KnowledgeActivityRegistered>>.All.Consume(
			IConsumeContext<KnowledgeActivityRegistered> context)
		{
			var message = new MailMessage();

			_logger.Info(string.Format("starting transformation to PDF for {0}", context.Message.VoucherNumber));

			var pdfPath = TransformToPdf(context.Message);

			_logger.Info("transformation to PDF done for {0}", context.Message.VoucherNumber);

			var fs = new FileStream(pdfPath, FileMode.Open, FileAccess.Read);
			try
			{
				message.Attachments.Add(new Attachment(fs, new ContentType("application/x-pdf")));
				_sender.Send(message);
			}
			finally
			{
				fs.Dispose();
			}

			context.Bus.Publish<MailSent>(new MailSentImpl());
		}

		string TransformToPdf(KnowledgeActivityRegistered message)
		{
			var filename = Path.GetFullPath(Path.Combine(Path.GetTempPath(), message.VoucherNumber));
			using (var fs = File.Create(filename + ".html"))
			{
				fs.Write(message.Voucher, 0, message.Voucher.Length);
				fs.Flush();
			}

			return RunCutyCapt(filename);
		}

		string RunCutyCapt(string filename)
		{
			var targetPdf = filename + ".pdf";

			var input = string.Format("--url=file:///{0}.html", filename.Replace("\\", "/"));
			var output = string.Format("--out={0}", targetPdf);
			var exe = Path.GetFullPath(".\\Renderer\\CutyCapt.exe");

			using (var p = _processManager.Start(exe, input, output))
				p.WaitForExit();

			return targetPdf;
		}
	}

	internal class MailSentImpl : MailSent
	{
	}
}