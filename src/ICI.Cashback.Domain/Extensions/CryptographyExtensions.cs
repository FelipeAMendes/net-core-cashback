using System.Security.Cryptography;
using System.Text;

namespace ICI.Cashback.Domain.Extensions
{
	public static class CryptographyExtensions
	{
		public static string Sha256Hash(string data)
		{
			using var sha256Hash = SHA256.Create();
			var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

			var builder = new StringBuilder();
			foreach (var @byte in bytes)
				builder.Append(@byte.ToString("x2"));

			return builder.ToString();
		}
	}
}
