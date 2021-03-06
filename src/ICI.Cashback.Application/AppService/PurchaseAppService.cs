using AutoMapper;
using ICI.Cashback.Application.Interfaces;
using ICI.Cashback.Application.ViewModels.Purchase;
using ICI.Cashback.Domain.Catalog.Messages;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Extensions;
using ICI.Cashback.Domain.Interfaces.Repositories;
using ICI.Cashback.Domain.Interfaces.Services;
using ICI.Cashback.Domain.Results;
using ICI.Cashback.Domain.Services.Purchases.Strategy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICI.Cashback.Application.AppService
{
	public class PurchaseAppService : IPurchaseAppService
	{
		private readonly IMapper _mapper;
		private readonly IPurchaseService _purchaseService;
		private readonly IPurchaseRepository _purchaseRepository;
		private readonly IPurchaseApiRepository _purchaseApiRepository;

		public PurchaseAppService(
			IMapper mapper,
			IPurchaseService purchaseService,
			IPurchaseRepository purchaseRepository,
			IPurchaseApiRepository purchaseApiRepository)
		{
			_mapper = mapper;
			_purchaseService = purchaseService;
			_purchaseRepository = purchaseRepository;
			_purchaseApiRepository = purchaseApiRepository;
		}

		public async Task<PurchaseViewModel> Find(int id)
		{
			var purchase = await _purchaseRepository
				.FindWithReseller(id);

			return _mapper.Map<PurchaseViewModel>(purchase);
		}

		public async Task<IEnumerable<PurchaseListViewModel>> List(int resellerId)
		{
			IEnumerable<Purchase> purchasesList = await _purchaseRepository
				.List(resellerId)
				.ToListAsync();

			var purchasesViewModel = _mapper.Map<IEnumerable<PurchaseListViewModel>>(purchasesList).ToList();
			var bonusStrategy = new BonusStrategyContext();
			foreach (var purchaseViewModel in purchasesViewModel)
			{
				var value = purchaseViewModel.Value.Replace(" ", "").Replace("R$", "");

				var (cashbackPercent, cashbackValue) = bonusStrategy.GetBonus(float.Parse(value));
				purchaseViewModel.CashbackPercent = cashbackPercent;
				purchaseViewModel.CashbackValue = cashbackValue.ToString("C");
			}

			return purchasesViewModel;
		}

		public PurchaseViewModel CreateViewModel(int resellerId, string resellerCpf)
		{
			return new PurchaseViewModel
			{
				Status = _purchaseService.GetPurchaseStatusByReseller(resellerCpf),
				ResellerId = resellerId,
				ResellerCpf = resellerCpf.FormatCpf()
			};
		}

		public async Task<string> GetAccumulatedCashback(string cpf)
		{
			var result = await _purchaseApiRepository.GetAccumulatedCashback(cpf);
			return result;
		}

		public async Task<Result<PurchaseViewModel>> Add(PurchaseViewModel viewModel)
		{
			var entity = _mapper.Map<Purchase>(viewModel);
			var result = await _purchaseService.Add(entity);

			if (!result.Success)
				viewModel.Errors = result.Object;

			return Result<PurchaseViewModel>.Create(result.Success, result.Message, viewModel);
		}

		public async Task<Result<PurchaseViewModel>> Edit(PurchaseViewModel viewModel)
		{
			var entity = _mapper.Map<Purchase>(viewModel);
			var result = await _purchaseService.Edit(entity);

			if (!result.Success)
				viewModel.Errors = result.Object;

			return Result<PurchaseViewModel>.Create(result.Success, result.Message, viewModel);
		}

		public async Task<Result<PurchaseViewModel>> Delete(int id)
		{
			var entity = await _purchaseRepository.Find(id);
			_purchaseRepository.Delete(entity);
			await _purchaseRepository.Save();
			return Result<PurchaseViewModel>.Create(true, PurchaseMessages.SuccessDelete);
		}
	}
}
