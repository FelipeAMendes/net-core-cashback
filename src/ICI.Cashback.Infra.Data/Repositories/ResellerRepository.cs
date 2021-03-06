using System.Threading.Tasks;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Interfaces.Repositories;
using ICI.Cashback.Domain.Interfaces.Repositories.Filters;
using Microsoft.EntityFrameworkCore;

namespace ICI.Cashback.Infra.Data.Repositories
{
	public class ResellerRepository : Repository<Reseller>, IResellerRepository
	{
		public ResellerRepository(DbContext context)
			: base(context)
		{
		}

		public async Task<Reseller> Find(string cpf, string password)
		{
			return await Context
				.Set<Reseller>()
				.FirstOrDefaultAsync(ResellerFilter.FilterByCpfAndPassword(cpf, password));
		}
	}
}
