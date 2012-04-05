using System.Collections.Generic;

namespace Agress.Messages.Commands
{
	public interface ReportAWeekOfTimes
	{
		/// <summary>
		/// A description of the activity
		/// </summary>
		string Description { get; }
		
		/// <summary>
		/// An array of 7 items, doubles,
		/// of the hours spent.
		/// </summary>
		IEnumerable<double> WeekHours { get; }

		/// <summary>
		/// Gets the accounting data, like what project and what role was specified.
		/// </summary>
		AccountingData Data { get; }

		/// <summary>
		/// Whether to click the save button/persist the changes made by the command's handler.
		/// </summary>
		bool SaveChanges { get; }
	}
}