using System.Linq;
using System.Threading.Tasks;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ICI.Cashback.Infra.Data.Repositories
{
	public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
	{

		public PurchaseRepository(DbContext context)
			: base(context)
		{
		}

		public async Task<Purchase> FindWithReseller(int resellerId)
		{
			return await Context
				.Set<Purchase>()
				.Include(p => p.Reseller)
				.FirstOrDefaultAsync();
		}

		public IQueryable<Purchase> List(int resellerId)
		{
			return Context
				.Set<Purchase>()
				.Include(p => p.Reseller)
				.Where(p => p.ResellerId == resellerId)
				.AsNoTracking();
		}
	}
}
