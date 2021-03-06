using System.Threading.Tasks;
using ICI.Cashback.Domain.Results;

namespace ICI.Cashback.Domain.Interfaces.Repositories
{
	public interface IApiHelper<T>
	{
		IApiHelper<T> CreateClient(string name);
		IApiHelper<T> UseBearer(string authToken);
		IApiHelper<T> UseGet();
		IApiHelper<T> UsePost();
		IApiHelper<T> UsePut();
		IApiHelper<T> UseDelete();
		IApiHelper<T> WithData(object data);
		Task<ApiResult<T>> Execute(string urlEndPoint);
	}
}
