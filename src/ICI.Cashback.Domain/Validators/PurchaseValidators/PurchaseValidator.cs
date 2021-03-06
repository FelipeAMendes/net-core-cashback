using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Extensions;
using ICI.Cashback.Domain.Specifications.PurchaseSpecs;

namespace ICI.Cashback.Domain.Validators.PurchaseValidators
{
	public class PurchaseValidator : IValidator
	{
		private readonly Purchase _purchase;

		public PurchaseValidator(Purchase reseller)
		{
			_purchase = reseller;
		}

		public bool Validate()
		{
			var rule =
				new IsCodeProvided()
					.And(new IsDateProvided()
						.And(new IsResellerIdProvided())
						.And(new IsValueProvided()));

			var isOk = rule.IsSatisfiedBy(_purchase);

			//if (!isOk)
			//	EventPublisher.OnRaiseNotificationEvent(new NotificationEventArgs("Another Message"));
			return isOk;
		}
	}
}
