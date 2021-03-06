using ICI.Cashback.Application.AppService;
using ICI.Cashback.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ICI.Cashback.Application.DependencyInjection
{
	public static class InfraDataInjections
	{
		public static IServiceCollection ApplicationDependencies(this IServiceCollection services)
		{
			services.AddScoped<IPurchaseAppService, PurchaseAppService>();
			services.AddScoped<IResellerAppService, ResellerAppService>();
			return services;
		}
	}
}
