using System.Linq;
using System.Threading.Tasks;
using ICI.Cashback.Domain.Entities;

namespace ICI.Cashback.Domain.Interfaces.Repositories
{
	public interface IPurchaseRepository : IRepository<Purchase>
	{
		Task<Purchase> FindWithReseller(int resellerId);
		IQueryable<Purchase> List(int resellerId);
	}
}
