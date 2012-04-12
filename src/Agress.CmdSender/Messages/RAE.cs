using Agress.Messages.Commands;

namespace Agress.CmdSender.Messages
{
	public class RAE
		: RegisterAccessoryExpense
	{
		public string Cause { get; set; }
		public string Comment { get; set; }
		public double Amount { get; set; }
	}
}