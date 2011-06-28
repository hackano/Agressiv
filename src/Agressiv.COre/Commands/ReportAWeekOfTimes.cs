using System;
using System.Collections.Generic;

namespace Agress.Core.Commands
{
	[Serializable]
	public class ReportAWeekOfTimes
	{
		protected ReportAWeekOfTimes()
		{
		}

		public ReportAWeekOfTimes(string description, IList<double> weekHours, AccountingData data, bool saveChanged = false)
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

		/// <summary>
		/// Gets the accounting data, like what project and what role was specified.
		/// </summary>
		public AccountingData Data { get; protected set; }

		/// <summary>
		/// Whether to click the save button/persist the changes made by the command's handler.
		/// </summary>
		public bool SaveChanges { get; protected set; }
	}
}