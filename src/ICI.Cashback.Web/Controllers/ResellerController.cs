using ICI.Cashback.Application.Interfaces;
using ICI.Cashback.Application.ViewModels.Reseller;
using ICI.Cashback.Domain.Enums;
using ICI.Cashback.Domain.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ICI.Cashback.Web.Controllers
{
	[AllowAnonymous]
	public class ResellerController : BaseController
	{
		private readonly IResellerAppService _resellerAppService;

		public ResellerController(IResellerAppService resellerAppService)
		{
			_resellerAppService = resellerAppService;
		}

		[Route("/")]
		[Route("/resellers")]
		[Route("/resellers/list")]
		public async Task<IActionResult> Index()
		{
			return View(await _resellerAppService.List());
		}

		[HttpGet]
		[Route("resellers/login")]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[Route("resellers/login")]
		public async Task<IActionResult> Login(ResellerLoginViewModel resellerViewModel)
		{
			var user = await _resellerAppService.Find(resellerViewModel.Cpf, resellerViewModel.Password);
			if (user is null)
			{
				ShowMessage(MessageType.Error, "Usuário ou senha inválidos".ToListString());
				return View();
			}

			_resellerAppService.SetToken(user);
			return RedirectToAction("Index", "Purchase", new {resellerId = user.Id});
		}

		[Route("resellers/logout")]
		public IActionResult Logout()
		{
			_resellerAppService.RemoveToken();
			return RedirectToAction("Index", "Reseller");
		}

		[Route("/resellers/create")]
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[Route("/resellers/create")]
		[HttpPost]
		public async Task<IActionResult> Create(ResellerViewModel resellerViewModel)
		{
			if (!ModelState.IsValid)
				return View(resellerViewModel);

			var result = await _resellerAppService.Add(resellerViewModel);

			if (result.Success)
			{
				ShowMessage(MessageType.Success, result.Message);
				return RedirectToAction(nameof(Index));
			}

			ShowMessage(MessageType.Error, result.Object.Errors);
			return View(resellerViewModel);
		}

		[Route("/resellers/edit/{id?}")]
		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null || id == 0)
				return RedirectToAction(nameof(Index));

			var resellerViewModel = await _resellerAppService.Find(id.Value);

			if (resellerViewModel is null)
				return RedirectToAction(nameof(Index));

			return View(resellerViewModel);
		}

		[Route("/resellers/edit")]
		[HttpPost]
		public async Task<IActionResult> Edit(ResellerViewModel resellerViewModel)
		{
			if (!ModelState.IsValid)
				return View(resellerViewModel);

			var result = await _resellerAppService.Edit(resellerViewModel);

			if (result.Success)
				return RedirectToAction(nameof(Index));

			ShowMessage(MessageType.Error, result.Object.Errors);
			return View(resellerViewModel);
		}

		[Route("/resellers/delete/{id?}")]
		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id is null || id == 0)
				return RedirectToAction(nameof(Index));

			var result = await _resellerAppService.Delete(id.Value);

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
