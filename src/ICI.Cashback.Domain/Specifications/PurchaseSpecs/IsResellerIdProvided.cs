using ICI.Cashback.Domain.Catalog.Messages;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Notifications;

namespace ICI.Cashback.Domain.Specifications.PurchaseSpecs
{
	public class IsResellerIdProvided : ISpecification<Purchase>
	{
		public bool IsSatisfiedBy(Purchase purchase)
		{
			var result = purchase.ResellerId > 0;
			if (!result)
				EventPublisher.OnRaiseNotificationEvent(new NotificationEventArgs(PurchaseMessages.ErrorResellerIdIsntProvided));
			return result;
		}
	}
}
