using ICI.Cashback.Domain;
using ICI.Cashback.Domain.Interfaces.Repositories;
using ICI.Cashback.Infra.Data.Contexts;
using ICI.Cashback.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ICI.Cashback.Infra.Data.DependencyInjection
{
	public static class InfraDataInjections
	{
		public static IServiceCollection InfraDataDependencies(this IServiceCollection services)
		{
			services.AddDbContext<CashbackDbContext>(options =>
				options.UseSqlite(CashbackConfiguration.ConnectionStrings.PrincipalConnection));
			services.AddScoped<DbContext, CashbackDbContext>();
			services.AddScoped<ILogRepository, LogRepository>();
			services.AddScoped<IPurchaseApiRepository, PurchaseApiRepository>();
			services.AddScoped<IPurchaseRepository, PurchaseRepository>();
			services.AddScoped<IResellerRepository, ResellerRepository>();

			return services;
		}
	}
}
