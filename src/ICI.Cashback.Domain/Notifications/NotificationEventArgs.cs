using System;

namespace ICI.Cashback.Domain.Notifications
{
	public class NotificationEventArgs : EventArgs
	{
		public NotificationEventArgs(string message)
		{
			Message = message;
		}

		public string Message { get; set; }
	}
}
