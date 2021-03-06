using System;
using ICI.Cashback.Domain.Catalog.Messages;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Notifications;

namespace ICI.Cashback.Domain.Specifications.PurchaseSpecs
{
	public class IsDateProvided : ISpecification<Purchase>
	{
		public bool IsSatisfiedBy(Purchase purchase)
		{
			var result = purchase.Date != DateTime.MinValue && purchase.Date != DateTime.MaxValue;
			if (!result)
				EventPublisher.OnRaiseNotificationEvent(new NotificationEventArgs(PurchaseMessages.ErrorDateIsntProvided));
			return result;
		}
	}
}
