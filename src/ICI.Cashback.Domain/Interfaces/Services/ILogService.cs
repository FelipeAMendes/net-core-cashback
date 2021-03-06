using ICI.Cashback.Domain.Enums;

namespace ICI.Cashback.Domain.Interfaces.Services
{
	public interface ILogService
	{
		void Add<T>(OperationLog operationLog, T @object, Platform platform);
	}
}
