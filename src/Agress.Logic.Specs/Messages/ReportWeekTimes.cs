using System.Collections.Generic;
using Agress.Messages.Commands;

namespace Agress.Logic.Specs.Messages
{
	internal class ReportWeekTimes
		: ReportAWeekOfTimes
	{
		public ReportWeekTimes(string description, IEnumerable<double> weekHours, AccountingData data)
		{
			Description = description;
			WeekHours = weekHours;
			Data = data;
			SaveChanges = true;
		}

		public string Description { get; private set; }
		public IEnumerable<double> WeekHours { get; private set; }
		public AccountingData Data { get; private set; }
		public bool SaveChanges { get; private set; }
	}
}