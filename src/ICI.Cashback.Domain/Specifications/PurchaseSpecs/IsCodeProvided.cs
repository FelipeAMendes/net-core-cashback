using ICI.Cashback.Domain.Catalog.Messages;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Extensions;
using ICI.Cashback.Domain.Notifications;

namespace ICI.Cashback.Domain.Specifications.PurchaseSpecs
{
	public class IsCodeProvided : ISpecification<Purchase>
	{
		public bool IsSatisfiedBy(Purchase purchase)
		{
			var result = !purchase.Code.IsNullOrWhiteSpace();
			if (!result)
				EventPublisher.OnRaiseNotificationEvent(new NotificationEventArgs(PurchaseMessages.ErrorCodeIsntProvided));
			return result;
		}
	}
}
