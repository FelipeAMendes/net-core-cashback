using ICI.Cashback.Domain.Notifications;

namespace ICI.Cashback.Domain.Interfaces.Services
{
	public abstract class Service<TEntity> : NotificationService
	{
		public abstract Notification Validate(TEntity entity);

		public void HandleNotificationEvent(object sender, NotificationEventArgs notificationEventArgs)
		{
			Notifier.Errors.Add(notificationEventArgs.Message);
		}
	}
}
