using AutoMapper;
using ICI.Cashback.Application.Interfaces;
using ICI.Cashback.Application.ViewModels.Reseller;
using ICI.Cashback.Domain;
using ICI.Cashback.Domain.Catalog.Messages;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Interfaces.Repositories;
using ICI.Cashback.Domain.Interfaces.Services;
using ICI.Cashback.Domain.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICI.Cashback.Application.AppService
{
	public class ResellerAppService : IResellerAppService
	{
		private readonly IMapper _mapper;
		private readonly IResellerService _resellerService;
		private readonly ITokenService _tokenService;
		private readonly IResellerRepository _resellerRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ResellerAppService(
			IMapper mapper,
			IResellerService resellerService,
			ITokenService tokenService,
			IResellerRepository resellerRepository,
			IHttpContextAccessor httpContextAccessor)
		{
			_mapper = mapper;
			_resellerService = resellerService;
			_tokenService = tokenService;
			_resellerRepository = resellerRepository;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<ResellerViewModel> Find(int id)
		{
			var reseller = await _resellerRepository
				.Find(id);

			return _mapper.Map<ResellerViewModel>(reseller);
		}

		public async Task<Reseller> Find(string cpf, string password)
		{
			return await _resellerService
				.Find(cpf, password);
		}

		public async Task<IEnumerable<ResellerListViewModel>> List()
		{
			IEnumerable<Reseller> resellersList = await _resellerRepository
				.List(r => r.Enabled == true)
				.ToListAsync();

			return _mapper.Map<IEnumerable<ResellerListViewModel>>(resellersList);
		}

		public async Task<Result<ResellerViewModel>> Add(ResellerViewModel viewModel)
		{
			var entity = _mapper.Map<Reseller>(viewModel);
			var result = await _resellerService.Add(entity);

			if (!result.Success)
				viewModel.Errors = result.Object;

			return Result<ResellerViewModel>.Create(result.Success, result.Message, viewModel);
		}

		public async Task<Result<ResellerViewModel>> Edit(ResellerViewModel viewModel)
		{
			var entity = await _resellerRepository.Find(viewModel.Id);
			entity.Name = viewModel.Name;
			entity.Email = viewModel.Email;
			var result = await _resellerService.Edit(entity);

			if (!result.Success)
				viewModel.Errors = result.Object;

			return Result<ResellerViewModel>.Create(result.Success, result.Message, viewModel);
		}

		public async Task<Result<ResellerViewModel>> Delete(int id)
		{
			var entity = await _resellerRepository.Find(id);
			_resellerRepository.Delete(entity);
			await _resellerRepository.Save();
			return Result<ResellerViewModel>.Create(true, ResellerMessages.SuccessDelete);
		}

		public void SetToken(Reseller reseller)
		{
			var token = _tokenService
				.Generate(reseller);

			var option = new CookieOptions {Expires = DateTime.Now.AddHours(2)};
			_httpContextAccessor.HttpContext.Response.Cookies.Append(CashbackConfiguration.Jwt.CookieName, token, option);
		}

		public void RemoveToken()
		{
			var hasCookie =
				_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(CashbackConfiguration.Jwt.CookieName);
			if (hasCookie)
				_httpContextAccessor.HttpContext.Response.Cookies.Delete(CashbackConfiguration.Jwt.CookieName);
		}
	}
}
