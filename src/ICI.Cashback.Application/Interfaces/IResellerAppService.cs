using System.Collections.Generic;
using System.Threading.Tasks;
using ICI.Cashback.Application.ViewModels.Reseller;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Results;

namespace ICI.Cashback.Application.Interfaces
{
	public interface IResellerAppService
	{
		Task<ResellerViewModel> Find(int id);
		Task<Reseller> Find(string cpf, string password);
		void SetToken(Reseller reseller);
		void RemoveToken();

		Task<IEnumerable<ResellerListViewModel>> List();

		Task<Result<ResellerViewModel>> Add(ResellerViewModel entity);
		Task<Result<ResellerViewModel>> Edit(ResellerViewModel entity);
		Task<Result<ResellerViewModel>> Delete(int id);
	}
}
