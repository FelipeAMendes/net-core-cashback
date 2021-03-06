using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;

namespace ICI.Cashback.Domain.Services.Resellers
{
	public class TokenService : ITokenService
	{
		public string Generate(Reseller reseller)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(CashbackConfiguration.Jwt.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, reseller.Cpf),
					new Claim(ClaimTypes.Role, reseller.Role)
				}),
				Expires = DateTime.UtcNow.AddHours(2),
				SigningCredentials =
					new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		public JwtSecurityToken Validate(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(CashbackConfiguration.Jwt.Secret);
			tokenHandler.ValidateToken(token, new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false,
				ClockSkew = TimeSpan.Zero
			}, out var validatedToken);

			return (JwtSecurityToken) validatedToken;
			//var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
		}
	}
}
