using System;

namespace Agress.Core.Commands
{
	[Serializable]
	public class AccountingData : IEquatable<AccountingData>
	{
		/// <summary>
		/// 	Type of report - overtime, ordinary time, sick time, etc.
		/// </summary>
		public string TimeCodeId { get; protected set; }

		/// <summary>
		/// 	What project is being debited.
		/// </summary>
		public string ProjectId { get; protected set; }

		/// <summary>
		/// 	What type of activity is being debited.
		/// </summary>
		public string ActivityId { get; protected set; }

		/// <summary>
		/// 	The role of the person during the activity.
		/// </summary>
		public int RoleId { get; protected set; }

		protected AccountingData()
		{
		}

		public AccountingData(string timeCodeId = "10", string projectId = "10100", string activityId = "10", int roleId = 2)
		{
			if (timeCodeId == null) throw new ArgumentNullException("timeCodeId");
			if (projectId == null) throw new ArgumentNullException("projectId");
			if (activityId == null) throw new ArgumentNullException("activityId");
			TimeCodeId = timeCodeId;
			ProjectId = projectId;
			ActivityId = activityId;
			RoleId = roleId;
		}

		public bool Equals(AccountingData other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.TimeCodeId, TimeCodeId) && Equals(other.ProjectId, ProjectId) && Equals(other.ActivityId, ActivityId) && other.RoleId == RoleId;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (AccountingData)) return false;
			return Equals((AccountingData) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = TimeCodeId.GetHashCode();
				result = (result*397) ^ ProjectId.GetHashCode();
				result = (result*397) ^ ActivityId.GetHashCode();
				result = (result*397) ^ RoleId;
				return result;
			}
		}

		public static bool operator ==(AccountingData left, AccountingData right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(AccountingData left, AccountingData right)
		{
			return !Equals(left, right);
		}
	}
}