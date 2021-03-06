using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ICI.Cashback.Domain.Interfaces.Repositories;
using ICI.Cashback.Domain.Results;
using Newtonsoft.Json;

namespace ICI.Cashback.Infra.Data.Repositories
{
	public class ApiHelper<T> : IApiHelper<T>
	{
		private enum HttpMethod
		{
			Get,
			Post,
			Put,
			Delete
		}

		private readonly IHttpClientFactory _httpClientFactory;

		private HttpClient _httpClient;
		private HttpMethod Method { get; set; }
		private object BodyData { get; set; }

		public ApiHelper(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public IApiHelper<T> CreateClient(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException(nameof(name));

			_httpClient = _httpClientFactory.CreateClient(name);
			return this;
		}

		public IApiHelper<T> UseBearer(string authToken)
		{
			if (authToken != null && _httpClient != null)
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authToken);

			return this;
		}

		public IApiHelper<T> UseGet()
		{
			Method = HttpMethod.Get;
			return this;
		}

		public IApiHelper<T> UsePost()
		{
			Method = HttpMethod.Post;
			return this;
		}

		public IApiHelper<T> UsePut()
		{
			Method = HttpMethod.Put;
			return this;
		}

		public IApiHelper<T> UseDelete()
		{
			Method = HttpMethod.Delete;
			return this;
		}

		public IApiHelper<T> WithData(object data)
		{
			BodyData = data;
			return this;
		}

		public async Task<ApiResult<T>> Execute(string urlEndPoint)
		{
			try
			{
				var response = Method switch
				{
					HttpMethod.Get => await _httpClient.GetAsync(urlEndPoint),
					HttpMethod.Post => await _httpClient.PostAsync(urlEndPoint,
						new StringContent(JsonConvert.SerializeObject(BodyData), Encoding.UTF8, "application/json")),
					HttpMethod.Put => await _httpClient.PutAsync(urlEndPoint,
						new StringContent(JsonConvert.SerializeObject(BodyData), Encoding.UTF8, "application/json")),
					HttpMethod.Delete => await _httpClient.DeleteAsync(urlEndPoint),
					_ => null
				};

				return await new ApiResult<T>().GetResponse(response);
			}
			catch (Exception ex)
			{
				return new ApiResult<T>(ex);
			}
		}
	}
}
