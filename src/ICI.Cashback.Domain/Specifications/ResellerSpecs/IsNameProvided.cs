using ICI.Cashback.Domain.Catalog.Messages;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Extensions;
using ICI.Cashback.Domain.Notifications;

namespace ICI.Cashback.Domain.Specifications.ResellerSpecs
{
	public class IsNameProvided : ISpecification<Reseller>
	{
		public bool IsSatisfiedBy(Reseller reseller)
		{
			var result = !reseller.Name.IsNullOrWhiteSpace();
			if (!result)
				EventPublisher.OnRaiseNotificationEvent(new NotificationEventArgs(ResellerMessages.ErrorNameIsntProvided));
			return result;
		}
	}
}
