using System.Threading.Tasks;

namespace ICI.Cashback.Domain.Interfaces.Repositories
{
	public interface IPurchaseApiRepository
	{
		Task<string> GetAccumulatedCashback(string cpf);
	}
}
