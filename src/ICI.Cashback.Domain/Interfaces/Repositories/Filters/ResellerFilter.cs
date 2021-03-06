using System;
using System.Linq.Expressions;
using ICI.Cashback.Domain.Entities;

namespace ICI.Cashback.Domain.Interfaces.Repositories.Filters
{
	public static class ResellerFilter
	{
		public static Expression<Func<Reseller, bool>> FilterByCpfAndPassword(string cpf, string password)
		{
			Expression<Func<Reseller, bool>> expression = reseller =>
				reseller.Cpf == cpf &&
				reseller.Password == password;

			return expression;
		}
	}
}
