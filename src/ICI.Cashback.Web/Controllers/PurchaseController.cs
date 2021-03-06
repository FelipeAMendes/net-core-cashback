using ICI.Cashback.Application.Interfaces;
using ICI.Cashback.Application.ViewModels.Purchase;
using ICI.Cashback.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ICI.Cashback.Web.Controllers
{
	[Authorize(Roles = "Reseller")]
	public class PurchaseController : BaseController
	{
		private readonly IPurchaseAppService _purchaseAppService;
		private readonly IResellerAppService _resellerAppService;

		public PurchaseController(IPurchaseAppService purchaseAppService, IResellerAppService resellerAppService)
		{
			_purchaseAppService = purchaseAppService;
			_resellerAppService = resellerAppService;
		}

		[Route("/purchases/reseller/{resellerId?}")]
		public async Task<IActionResult> Index(int? resellerId)
		{
			if (resellerId is null || resellerId == 0)
				return RedirectToAction(nameof(Index));

			var resellerViewModel = await _resellerAppService.Find(resellerId.Value);
			if (!(resellerViewModel.Id > 0))
				return RedirectToAction("Index", "Reseller");

			TempData["ResellerId"] = resellerId;
			var purchases = await _purchaseAppService.List(resellerViewModel.Id);
			return View(purchases);
		}

		[Route("/purchases/reseller/{resellerId?}/cashback-acumulado")]
		public async Task<IActionResult> AccumulatedCashback(int? resellerId)
		{
			if (resellerId is null || resellerId == 0)
				return RedirectToAction(nameof(Index));

			var resellerViewModel = await _resellerAppService.Find(resellerId.Value);
			if (!(resellerViewModel.Id > 0))
				return RedirectToAction("Index", "Reseller");

			TempData["AccumulatedCashback"] = await _purchaseAppService.GetAccumulatedCashback(resellerViewModel.Cpf);
			return View(resellerViewModel);
		}

		[Route("/purchases/create/{resellerId?}")]
		[HttpGet]
		public async Task<IActionResult> Create(int? resellerId)
		{
			if (resellerId is null || resellerId == 0)
				return RedirectToAction(nameof(Index));

			var resellerViewModel = await _resellerAppService.Find(resellerId.Value);
			if (!(resellerViewModel.Id > 0))
				return RedirectToAction("Index", "Reseller");

			var viewModel = _purchaseAppService.CreateViewModel(resellerViewModel.Id, resellerViewModel.Cpf);
			return View(viewModel);
		}

		[Route("/purchases/create/{resellerId?}")]
		[HttpPost]
		public async Task<IActionResult> Create(int? resellerId, PurchaseViewModel purchaseViewModel)
		{
			if (!ModelState.IsValid)
				return View(purchaseViewModel);

			var result = await _purchaseAppService.Add(purchaseViewModel);

			if (result.Success)
			{
				ShowMessage(MessageType.Success, result.Message);
				return RedirectToAction(nameof(Index), new {resellerId = purchaseViewModel.ResellerId});
			}

			ShowMessage(MessageType.Error, result.Object.Errors);
			return View(purchaseViewModel);
		}

		[Route("/purchases/edit/{id?}")]
		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null || id == 0)
				return RedirectToAction(nameof(Index));

			var purchaseViewModel = await _purchaseAppService.Find(id.Value);

			if (purchaseViewModel is null)
				return RedirectToAction(nameof(Index));

			return View(purchaseViewModel);
		}

		[Route("/purchases/edit")]
		[HttpPost]
		public async Task<IActionResult> Edit(PurchaseViewModel purchaseViewModel)
		{
			if (!ModelState.IsValid)
				return View(purchaseViewModel);

			var result = await _purchaseAppService.Edit(purchaseViewModel);

			if (result.Success)
			{
				ShowMessage(MessageType.Success, result.Message);
				return RedirectToAction(nameof(Index));
			}

			ShowMessage(MessageType.Error, result.Object.Errors);
			return View(purchaseViewModel);
		}

		[Route("/purchases/delete/{id?}")]
		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id is null || id == 0)
				return RedirectToAction(nameof(Index));

			var result = await _purchaseAppService.Delete(id.Value);

			if (result.Success)
			{
				ShowMessage(MessageType.Success, result.Message);
				return RedirectToAction(nameof(Index));
			}

			ShowMessage(MessageType.Error, result.Object.Errors);
			return RedirectToAction(nameof(Index));
		}
	}
}
