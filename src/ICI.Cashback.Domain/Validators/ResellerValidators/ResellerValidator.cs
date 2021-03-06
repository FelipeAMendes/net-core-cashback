using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Extensions;
using ICI.Cashback.Domain.Interfaces.Repositories;
using ICI.Cashback.Domain.Specifications.ResellerSpecs;

namespace ICI.Cashback.Domain.Validators.ResellerValidators
{
	public class ResellerValidator : IValidator
	{
		private readonly Reseller _reseller;
		private readonly IResellerRepository _resellerRepository;

		public ResellerValidator(Reseller reseller, IResellerRepository resellerRepository)
		{
			_reseller = reseller;
			_resellerRepository = resellerRepository;
		}

		public bool Validate()
		{
			var rule =
				new IsCpfProvided()
					.And(new IsNameProvided()
						.And(new IsEmailProvided())
						.And(new IsCpfValid())
						.And(new IsEmailValid())
						.And(new ExistingCpf(_resellerRepository))
						.And(new ExistingEmail(_resellerRepository)));

			return rule.IsSatisfiedBy(_reseller);
		}
	}
}
