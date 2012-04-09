using System;
using Agress.Messages.Events;

namespace Agress.WebDriver
{
	public class KnowledgeActivityReplyEvent
		: KnowledgeActivityRegistered
	{
		public string VoucherNumber { get; set; }
		public byte[] Voucher { get; set; }
		public string UserName { get; set; }
		public Uri Scan { get; set; }
	}
}