using System.Collections.Generic;
using System.Threading.Tasks;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Enums;
using ICI.Cashback.Domain.Results;

namespace ICI.Cashback.Domain.Interfaces.Services
{
	public interface IPurchaseService
	{
		PurchaseStatus GetPurchaseStatusByReseller(string cpf);
		Task<Result<IEnumerable<string>>> Add(Purchase purchase);
		Task<Result<IEnumerable<string>>> Edit(Purchase purchase);
	}
}
