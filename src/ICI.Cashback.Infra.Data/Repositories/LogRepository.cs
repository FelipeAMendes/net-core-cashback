using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ICI.Cashback.Infra.Data.Repositories
{
	public class LogRepository : Repository<Log>, ILogRepository
	{
		public LogRepository(DbContext context)
			: base(context)
		{
		}
	}
}
