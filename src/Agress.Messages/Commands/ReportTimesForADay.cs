using System;

namespace Agress.Messages.Commands
{
	public interface ReportTimesForADay
	{
		/// <summary>
		/// Gets the day of the week to report for.
		/// </summary>
		DayOfWeek Day { get; }

		/// <summary>
		/// Gets the number of hours worked this day.
		/// </summary>
		double Hours { get; }

		/// <summary>
		/// A description of the activity
		/// </summary>
		string Description { get; }

		/// <summary>
		/// A description that the actual human client paying should be able
		/// to read.
		/// </summary>
		string LongDescription { get; }

		/// <summary>
		/// The role of the person during the activity.
		/// </summary>
		AccountingData Data { get; }

		/// <summary>
		/// Whether to save the data in Agresso when handling the command.
		/// </summary>
		bool SaveData { get; }
	}
}