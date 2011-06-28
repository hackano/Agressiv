using System;

namespace Agress.Core.Commands
{
	[Serializable]
	public class AccountingData
	{

		/// <summary>
		/// Type of report - overtime, ordinary time, sick time, etc.
		/// </summary>
		public string TimeCodeId { get; protected set; }
		
		/// <summary>
		/// What project is being debited.
		/// </summary>
		public string ProjectId { get; protected set; }

		/// <summary>
		/// What type of activity is being debited.
		/// </summary>
		public string ActivityId { get; protected set; }

		/// <summary>
		/// The role of the person during the activity.
		/// </summary>
		public int RoleId { get; protected set; }

		protected AccountingData()
		{
		}

		public AccountingData(string timeCodeId = "10", string projectId = "10100", string activityId = "10", int roleId = 2)
		{
			this.TimeCodeId = timeCodeId;
			this.ProjectId = projectId;
			this.ActivityId = activityId;
			this.RoleId = roleId;
		}
	}
}