using System.Collections.Generic;

namespace Agress.Messages.Events
{
	public interface FullWeekReported
	{
		IEnumerable<double> Hours { get; }
	}
}