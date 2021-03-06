using ICI.Cashback.Domain.Catalog.Messages;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Extensions;
using ICI.Cashback.Domain.Notifications;

namespace ICI.Cashback.Domain.Specifications.ResellerSpecs
{
	public class IsCpfValid : ISpecification<Reseller>
	{
		public bool IsSatisfiedBy(Reseller reseller)
		{
			var result = reseller.Cpf.IsValidCpf();
			if (!result)
				EventPublisher.OnRaiseNotificationEvent(new NotificationEventArgs(ResellerMessages.ErrorCpfInvalid));
			return result;
		}
	}
}
