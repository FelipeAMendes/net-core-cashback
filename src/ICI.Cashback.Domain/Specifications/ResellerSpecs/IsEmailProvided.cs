using ICI.Cashback.Domain.Catalog.Messages;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Extensions;
using ICI.Cashback.Domain.Notifications;

namespace ICI.Cashback.Domain.Specifications.ResellerSpecs
{
	public class IsEmailProvided : ISpecification<Reseller>
	{
		public bool IsSatisfiedBy(Reseller reseller)
		{
			var result = !reseller.Email.IsNullOrWhiteSpace();
			if (!result)
				EventPublisher.OnRaiseNotificationEvent(new NotificationEventArgs(ResellerMessages.ErrorEmailIsntProvided));
			return result;
		}
	}
}
