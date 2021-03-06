using ICI.Cashback.Domain.Catalog.Messages;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Extensions;
using ICI.Cashback.Domain.Notifications;

namespace ICI.Cashback.Domain.Specifications.ResellerSpecs
{
	public class IsEmailValid : ISpecification<Reseller>
	{
		public bool IsSatisfiedBy(Reseller reseller)
		{
			var result = reseller.Email.IsValidEmail();
			if (!result)
				EventPublisher.OnRaiseNotificationEvent(new NotificationEventArgs(ResellerMessages.ErrorEmailInvalid));
			return result;
		}
	}
}
