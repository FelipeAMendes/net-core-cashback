using ICI.Cashback.Domain.Interfaces.Services;
using ICI.Cashback.Domain.Services;
using ICI.Cashback.Domain.Services.Purchases;
using ICI.Cashback.Domain.Services.Resellers;
using Microsoft.Extensions.DependencyInjection;

namespace ICI.Cashback.Domain.DependencyInjection
{
	public static class InfraDataInjections
	{
		public static IServiceCollection DomainDependencies(this IServiceCollection services)
		{
			services.AddScoped<ILogService, LogService>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IPurchaseService, PurchaseService>();
			services.AddScoped<IResellerService, ResellerService>();
			return services;
		}
	}
}
