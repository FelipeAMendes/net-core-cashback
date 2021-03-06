using System.Linq;
using ICI.Cashback.Domain.Catalog.Messages;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Interfaces.Repositories;
using ICI.Cashback.Domain.Notifications;

namespace ICI.Cashback.Domain.Specifications.ResellerSpecs
{
	public class ExistingCpf : ISpecification<Reseller>
	{
		private readonly IResellerRepository _resellerRepository;

		public ExistingCpf(IResellerRepository resellerRepository)
		{
			_resellerRepository = resellerRepository;
		}

		public bool IsSatisfiedBy(Reseller reseller)
		{
			var result = _resellerRepository
				.List(r => r.Cpf == reseller.Cpf)
				.FirstOrDefault();

			if (result?.Id > 0)
				EventPublisher.OnRaiseNotificationEvent(new NotificationEventArgs(ResellerMessages.ErrorExistingCpf));

			return !(result?.Id > 0);
		}
	}
}
