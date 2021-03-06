using System.Collections.Generic;
using System.Threading.Tasks;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Results;

namespace ICI.Cashback.Domain.Interfaces.Services
{
	public interface IResellerService
	{
		Task<Reseller> Find(string cpf, string password);
		Task<Result<IEnumerable<string>>> Add(Reseller reseller);
		Task<Result<IEnumerable<string>>> Edit(Reseller reseller);
	}
}
