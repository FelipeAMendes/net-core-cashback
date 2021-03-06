using System.Collections.Generic;

namespace ICI.Cashback.Domain.Notifications
{
	public class Notification
	{
		public IList<string> Errors { get; set; }

		public Notification()
		{
			Errors = new List<string>();
		}
	}
}
