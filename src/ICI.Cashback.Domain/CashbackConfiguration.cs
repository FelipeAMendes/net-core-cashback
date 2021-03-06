using Microsoft.Extensions.Configuration;

namespace ICI.Cashback.Domain
{
	public static class CashbackConfiguration
	{
		public static void Configure(IConfiguration configuration)
		{
			AddConfigurations(configuration);
		}

		public static void AddConfigurations(IConfiguration configuration)
		{
			ApiCashback = new ApiCashbackConfiguration
			{
				Name = configuration
					?.GetSection($"{ApiCashbackConfiguration.ApiCashback}:{nameof(ApiCashbackConfiguration.Name)}")?.Value,
				Url = configuration
					?.GetSection($"{ApiCashbackConfiguration.ApiCashback}:{nameof(ApiCashbackConfiguration.Url)}")?.Value,
				Version = configuration
					?.GetSection($"{ApiCashbackConfiguration.ApiCashback}:{nameof(ApiCashbackConfiguration.Version)}")?.Value,
				Token = configuration
					?.GetSection($"{ApiCashbackConfiguration.ApiCashback}:{nameof(ApiCashbackConfiguration.Token)}")?.Value
			};

			ConnectionStrings = new ConnectionStringsConfiguration
			{
				PrincipalConnection = configuration
					?.GetSection(
						$"{ConnectionStringsConfiguration.ConnectionStrings}:{nameof(ConnectionStringsConfiguration.PrincipalConnection)}")
					?.Value
			};

			Jwt = new JwtConfiguration
			{
				Secret = configuration?.GetSection($"{JwtConfiguration.Jwt}:{nameof(JwtConfiguration.Secret)}")?.Value,
				CookieName = configuration?.GetSection($"{JwtConfiguration.Jwt}:{nameof(JwtConfiguration.CookieName)}")?.Value
			};
		}

		public static ApiCashbackConfiguration ApiCashback;
		public static ConnectionStringsConfiguration ConnectionStrings;
		public static JwtConfiguration Jwt;
	}

	public class ApiCashbackConfiguration
	{
		public const string ApiCashback = "Api:Cashback";
		public string Name { get; set; }
		public string Url { get; set; }
		public string Version { get; set; }
		public string Token { get; set; }
	}

	public class ConnectionStringsConfiguration
	{
		public const string ConnectionStrings = "ConnectionStrings";
		public string PrincipalConnection { get; set; }
	}

	public class JwtConfiguration
	{
		public const string Jwt = "Jwt";
		public string Secret { get; set; }
		public string CookieName { get; set; }
	}
}