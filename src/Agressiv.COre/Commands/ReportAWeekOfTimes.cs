using System;
using System.Collections.Generic;

namespace Agress.Core.Commands
{
	[Serializable]
	public class ReportAWeekOfTimes
	{
		public ReportAWeekOfTimes(string timeCodeId, string projectId, string activityId, string description, string roleId, IList<double> weekHours)
		{
			TimeCodeId = timeCodeId;
			ProjectId = projectId;
			ActivityId = activityId;
			Description = description;
			RoleId = roleId;
			WeekHours = weekHours;
		}

		/// <summary>
		/// Type of report - overtime, ordinary time, sick time, etc.
		/// </summary>
		public string TimeCodeId { get; private set; }

		/// <summary>
		/// What project is being debited.
		/// </summary>
		public string ProjectId { get; private set; }

		/// <summary>
		/// What type of activity is being debited.
		/// </summary>
		public string ActivityId { get; private set; }
		
		/// <summary>
		/// A description of the activity
		/// </summary>
		public string Description { get; private set; }
		
		/// <summary>
		/// The role of the person during the activity.
		/// </summary>
		public string RoleId { get; private set; }

		/// <summary>
		/// An array of 7 items, doubles,
		/// of the hours spent.
		/// </summary>
		public IList<double> WeekHours { get; private set; }
	}
}