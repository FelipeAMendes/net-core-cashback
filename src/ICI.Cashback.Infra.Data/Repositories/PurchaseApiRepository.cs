using System.Net.Http;
using System.Threading.Tasks;
using ICI.Cashback.Domain;
using ICI.Cashback.Domain.Catalog.Endpoints;
using ICI.Cashback.Domain.Interfaces.Repositories;

namespace ICI.Cashback.Infra.Data.Repositories
{
	public class PurchaseApiRepository : IPurchaseApiRepository
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public PurchaseApiRepository(IHttpClientFactory httpClientFactory)
		{
			this._httpClientFactory = httpClientFactory;
		}

		public async Task<string> GetAccumulatedCashback(string cpf)
		{
			var url =
				$"{CashbackConfiguration.ApiCashback.Url}{CashbackConfiguration.ApiCashback.Version}{string.Format(ApiCashbackEndpoint.CashbackUrl, cpf)}";

			var apiHelper = new ApiHelper<string>(_httpClientFactory);

			var result = await apiHelper
				.CreateClient(CashbackConfiguration.ApiCashback.Name)
				.UseGet()
				.UseBearer(CashbackConfiguration.ApiCashback.Token)
				.Execute(url);

			return result.Data;
		}
	}
}
