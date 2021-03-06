using ICI.Cashback.Domain.Catalog.Messages;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Notifications;

namespace ICI.Cashback.Domain.Specifications.PurchaseSpecs
{
	public class IsValueProvided : ISpecification<Purchase>
	{
		public bool IsSatisfiedBy(Purchase purchase)
		{
			var result = purchase.Value > 0;
			if (!result)
				EventPublisher.OnRaiseNotificationEvent(new NotificationEventArgs(PurchaseMessages.ErrorValueIsntProvided));
			return result;
		}
	}
}
