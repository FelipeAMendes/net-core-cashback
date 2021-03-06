using System.Collections.Generic;
using System.Threading.Tasks;
using ICI.Cashback.Application.ViewModels.Purchase;
using ICI.Cashback.Domain.Results;

namespace ICI.Cashback.Application.Interfaces
{
	public interface IPurchaseAppService
	{
		Task<PurchaseViewModel> Find(int id);

		Task<IEnumerable<PurchaseListViewModel>> List(int resellerId);

		Task<Result<PurchaseViewModel>> Add(PurchaseViewModel entity);
		Task<Result<PurchaseViewModel>> Edit(PurchaseViewModel entity);
		Task<Result<PurchaseViewModel>> Delete(int id);

		PurchaseViewModel CreateViewModel(int resellerId, string resellerCpf);
		Task<string> GetAccumulatedCashback(string cpf);
	}
}
