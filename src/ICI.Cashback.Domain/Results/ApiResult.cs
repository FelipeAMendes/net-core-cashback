using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ICI.Cashback.Domain.Results
{
	public class ApiResult<T>
	{
		public T Data { get; private set; }
		public string ErrorMessage { get; private set; }
		public bool Success { get; private set; }

		public ApiResult()
		{
			Success = false;
		}

		public ApiResult(Exception ex) : this()
		{
			ErrorMessage = ex.Message;
		}

		public async Task<ApiResult<T>> GetResponse(HttpResponseMessage httpResponse)
		{
			using (httpResponse)
			{
				Success = httpResponse.IsSuccessStatusCode;

				if (Success)
					Data = JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());
				else
					ErrorMessage = await httpResponse.Content.ReadAsStringAsync();
			}

			return this;
		}
	}
}
