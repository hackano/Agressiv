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
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Agress.Messages.Events;
using Agress.Messages.Mailer;
using MassTransit;
using MassTransit.Util;
using NLog;
using Magnum.Extensions;

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

			var scanPath = context.Message.Scan.LocalPath;
			message.Subject = string.Format("Registered voucher#{0}", context.Message.VoucherNumber);
			message.Body = message.Subject;
			message.Attachments.Add(
				AttachmentHelper.CreateAttachment(pdfPath,
					Path.GetFileName(pdfPath),
					TransferEncoding.Base64));
			message.Attachments.Add(
				AttachmentHelper.CreateAttachment(scanPath,
					Path.GetFileName(scanPath),
					TransferEncoding.Base64));
				
			_sender.Send(message, context.Message.UserName);

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

	public class AttachmentHelper
	{
		// http://social.msdn.microsoft.com/Forums/en-US/dotnetframeworkde/thread/b6c764f7-4697-4394-b45f-128a24306d55
		public static Attachment CreateAttachment(string attachmentFile, string displayName, TransferEncoding transferEncoding)
		{
			var attachment = new Attachment(attachmentFile);
			attachment.TransferEncoding = transferEncoding;

			string tranferEncodingMarker;
			string encodingMarker;
			int maxChunkLength;

			switch (transferEncoding)
			{
				case TransferEncoding.Base64:
					tranferEncodingMarker = "B";
					encodingMarker = "UTF-8";
					maxChunkLength = 30;
					break;
				case TransferEncoding.QuotedPrintable:
					tranferEncodingMarker = "Q";
					encodingMarker = "ISO-8859-1";
					maxChunkLength = 76;
					break;
				default:
					throw (new ArgumentException(String.Format("The specified TransferEncoding is not supported: {0}", transferEncoding, "transferEncoding")));
			}

			attachment.NameEncoding = Encoding.GetEncoding(encodingMarker);

			string encodingtoken = String.Format("=?{0}?{1}?", encodingMarker, tranferEncodingMarker);
			const string softbreak = "?=";
			
			string encodedAttachmentName;

			if (attachment.TransferEncoding == TransferEncoding.QuotedPrintable)
				encodedAttachmentName = Uri.EscapeUriString(displayName)
					.Replace("+", " ").Replace("%", "=");
			else
				encodedAttachmentName = Convert.ToBase64String(Encoding.UTF8.GetBytes(displayName));

			encodedAttachmentName = SplitEncodedAttachmentName(encodingtoken, softbreak, maxChunkLength, encodedAttachmentName);
			attachment.Name = encodedAttachmentName;

			return attachment;
		}

		private static string SplitEncodedAttachmentName(string encodingtoken, string softbreak, int maxChunkLength, string encoded)
		{
			int splitLength = maxChunkLength - encodingtoken.Length - (softbreak.Length * 2);
			var parts = SplitByLength(encoded, splitLength);

			string encodedAttachmentName = encodingtoken;

			foreach (var part in parts)
				encodedAttachmentName += part + softbreak + encodingtoken;

			encodedAttachmentName = encodedAttachmentName.Remove(encodedAttachmentName.Length - encodingtoken.Length, encodingtoken.Length);
			return encodedAttachmentName;
		}

		private static IEnumerable<string> SplitByLength(string stringToSplit, int length)
		{
			while (stringToSplit.Length > length)
			{
				yield return stringToSplit.Substring(0, length);
				stringToSplit = stringToSplit.Substring(length);
			}

			if (stringToSplit.Length > 0) yield return stringToSplit;
		}
	}
}