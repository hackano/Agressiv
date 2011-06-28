using System;
using System.Collections.Generic;

namespace Agress.Core.Events
{
	[Serializable]
	public class FullWeekReported
	{
		public IEnumerable<double> Hours { get; protected set; }

		public FullWeekReported()
		{
		}

		public FullWeekReported(IEnumerable<double> hours)
		{
			Hours = hours;
		}
	}
}