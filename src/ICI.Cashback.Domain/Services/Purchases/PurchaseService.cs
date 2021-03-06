using System.Collections.Generic;
using System.Threading.Tasks;
using ICI.Cashback.Domain.Catalog.Messages;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Enums;
using ICI.Cashback.Domain.Interfaces.Repositories;
using ICI.Cashback.Domain.Interfaces.Services;
using ICI.Cashback.Domain.Notifications;
using ICI.Cashback.Domain.Results;
using ICI.Cashback.Domain.Validators.PurchaseValidators;

namespace ICI.Cashback.Domain.Services.Purchases
{
	public class PurchaseService : Service<Purchase>, IPurchaseService
	{
		private readonly IPurchaseRepository _purchaseRepository;
		private readonly ILogService _logService;

		public PurchaseService(IPurchaseRepository purchaseRepository, ILogService logService)
		{
			_purchaseRepository = purchaseRepository;
			_logService = logService;
		}

		public async Task<Result<IEnumerable<string>>> Add(Purchase purchase)
		{
			var resultValidation = Validate(purchase);
			if (resultValidation.Errors.Count > 0)
				return Result<IEnumerable<string>>.Create(false, resultValidation.Errors);

			_purchaseRepository.Add(purchase);
			_logService.Add(OperationLog.Insert, purchase, Platform.Web);
			await _purchaseRepository.Save();
			return Result<IEnumerable<string>>.Create(true, PurchaseMessages.SuccessRegister);
		}

		public async Task<Result<IEnumerable<string>>> Edit(Purchase purchase)
		{
			var resultValidation = Validate(purchase);
			if (resultValidation.Errors.Count > 0)
				return Result<IEnumerable<string>>.Create(false, resultValidation.Errors);

			_purchaseRepository.Update(purchase);
			_logService.Add(OperationLog.Update, purchase, Platform.Web);
			await _purchaseRepository.Save();
			return Result<IEnumerable<string>>.Create(true, PurchaseMessages.SuccessEdit);
		}

		public PurchaseStatus GetPurchaseStatusByReseller(string cpf)
		{
			/*
			 * .....
			*/
			const string cpfMaster = "15350946056";

			return string.Equals(cpf, cpfMaster)
				? PurchaseStatus.Approved
				: PurchaseStatus.InValidation;
		}

		public override Notification Validate(Purchase purchase)
		{
			var validator = new PurchaseValidator(purchase);
			EventPublisher.RaiseNotificationEvent += HandleNotificationEvent;
			_ = validator.Validate();
			return Notifier;
		}
	}
}
