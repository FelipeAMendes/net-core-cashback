using System.Threading.Tasks;
using ICI.Cashback.Domain.Entities;

namespace ICI.Cashback.Domain.Interfaces.Repositories
{
	public interface IResellerRepository : IRepository<Reseller>
	{
		Task<Reseller> Find(string cpf, string password);
	}
}
