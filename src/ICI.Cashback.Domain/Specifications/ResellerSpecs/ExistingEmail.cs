using System.Linq;
using ICI.Cashback.Domain.Catalog.Messages;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Interfaces.Repositories;
using ICI.Cashback.Domain.Notifications;

namespace ICI.Cashback.Domain.Specifications.ResellerSpecs
{
	public class ExistingEmail : ISpecification<Reseller>
	{
		private readonly IResellerRepository _resellerRepository;

		public ExistingEmail(IResellerRepository resellerRepository)
		{
			_resellerRepository = resellerRepository;
		}

		public bool IsSatisfiedBy(Reseller reseller)
		{
			var result = _resellerRepository
				.List(r => r.Email == reseller.Email)
				.FirstOrDefault();

			if (result?.Id > 0)
				EventPublisher.OnRaiseNotificationEvent(new NotificationEventArgs(ResellerMessages.ErrorExistingEmail));

			return !(result?.Id > 0);
		}
	}
}
