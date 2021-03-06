using System.IdentityModel.Tokens.Jwt;
using ICI.Cashback.Domain.Entities;

namespace ICI.Cashback.Domain.Interfaces.Services
{
	public interface ITokenService
	{
		string Generate(Reseller reseller);
		JwtSecurityToken Validate(string token);
	}
}
