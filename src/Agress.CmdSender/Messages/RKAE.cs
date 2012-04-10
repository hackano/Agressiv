using System;
using Agress.Messages.Commands;

namespace Agress.CmdSender.Messages
{
	public class RKAE
		: RegisterKnowledgeActivityExpense
	{
		public string Cause { get; set; }
		public string Comment { get; set; }
		public double Amount { get; set; }
		public long Epoch { get; set; }
		public int TargetProject { get; set; }
		public bool SubmitFinal { get; set; }
		public Uri Scan { get; set; }
	}
}