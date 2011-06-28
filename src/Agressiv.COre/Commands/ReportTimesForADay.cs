using System;

namespace Agress.Core.Commands
{
	[Serializable]
	public class ReportTimesForADay
	{
		/// <summary>
		/// Gets the day of the week to report for.
		/// </summary>
		public DayOfWeek Day { get; protected set; }

		/// <summary>
		/// Gets the number of hours worked this day.
		/// </summary>
		public double Hours { get; protected set; }

		/// <summary>
		/// A description of the activity
		/// </summary>
		public string Description { get; protected set; }

		/// <summary>
		/// A description that the actual human client paying should be able
		/// to read.
		/// </summary>
		public string LongDescription { get; protected set; }

		/// <summary>
		/// The role of the person during the activity.
		/// </summary>
		public AccountingData Data { get; protected set; }

		/// <summary>
		/// Whether to save the data in Agresso when handling the command.
		/// </summary>
		public bool SaveData { get; protected set; }


		protected ReportTimesForADay()
		{
		}

		public ReportTimesForADay(DayOfWeek whatDay, double hours, string description, 
			string longDescription,
			AccountingData data,
			bool saveData = false)
		{
			Day = whatDay;
			Hours = hours;
			Description = description;
			LongDescription = longDescription;
			Data = data;
			SaveData = saveData;
		}
	}
}