using System;
using System.Collections.Generic;

namespace Agress.Core.Commands
{
	[Serializable]
	public class ReportAWeekOfTimes
	{
		public ReportAWeekOfTimes()
		{
		}

		public ReportAWeekOfTimes(string description, IList<double> weekHours, AccountingData data)
		{
			Description = description;
			WeekHours = weekHours;
			Data = data;
		}

		/// <summary>
		/// A description of the activity
		/// </summary>
		public string Description { get; protected set; }
		
		
		/// <summary>
		/// An array of 7 items, doubles,
		/// of the hours spent.
		/// </summary>
		public IList<double> WeekHours { get; protected set; }


		public AccountingData Data { get; protected set; }
	}
}