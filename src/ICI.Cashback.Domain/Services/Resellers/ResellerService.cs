using System.Collections.Generic;
using System.Threading.Tasks;
using ICI.Cashback.Domain.Catalog.Messages;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Enums;
using ICI.Cashback.Domain.Interfaces.Repositories;
using ICI.Cashback.Domain.Interfaces.Services;
using ICI.Cashback.Domain.Notifications;
using ICI.Cashback.Domain.Results;
using ICI.Cashback.Domain.Validators.ResellerValidators;

namespace ICI.Cashback.Domain.Services.Resellers
{
	public class ResellerService : Service<Reseller>, IResellerService
	{
		private readonly IResellerRepository _resellerRepository;
		private readonly ILogService _logService;

		public ResellerService(IResellerRepository resellerRepository, ILogService logService)
		{
			_resellerRepository = resellerRepository;
			_logService = logService;
		}

		public async Task<Reseller> Find(string cpf, string password)
		{
			var reseller = new Reseller
			{
				Cpf = cpf,
				Password = password
			};

			reseller.Clear().SetPassword();
			return await _resellerRepository.Find(reseller.Cpf, reseller.Password);
		}

		public async Task<Result<IEnumerable<string>>> Add(Reseller reseller)
		{
			reseller.Clear().SetRole().SetPassword();
			var resultValidation = Validate(reseller);
			if (resultValidation.Errors.Count > 0)
				return Result<IEnumerable<string>>.Create(false, resultValidation.Errors);

			_resellerRepository.Add(reseller);
			_logService.Add(OperationLog.Insert, reseller, Platform.Web);
			await _resellerRepository.Save();
			return Result<IEnumerable<string>>.Create(true, ResellerMessages.SuccessRegister);
		}

		public async Task<Result<IEnumerable<string>>> Edit(Reseller reseller)
		{
			reseller.Clear();
			var resultValidation = Validate(reseller);
			if (resultValidation.Errors.Count > 0)
				return Result<IEnumerable<string>>.Create(false, resultValidation.Errors);

			_resellerRepository.Update(reseller);
			_logService.Add(OperationLog.Update, reseller, Platform.Web);
			await _resellerRepository.Save();
			return Result<IEnumerable<string>>.Create(true, ResellerMessages.SuccessEdit);
		}

		public override Notification Validate(Reseller reseller)
		{
			var validator = new ResellerValidator(reseller, _resellerRepository);
			EventPublisher.RaiseNotificationEvent += HandleNotificationEvent;
			_ = validator.Validate();
			return Notifier;
		}
	}
}
