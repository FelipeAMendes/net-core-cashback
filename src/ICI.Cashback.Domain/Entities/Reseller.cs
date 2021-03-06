using System.Collections.Generic;
using ICI.Cashback.Domain.Extensions;

namespace ICI.Cashback.Domain.Entities
{
	public class Reseller : Entity
	{
		public string Name { get; set; }
		public string Cpf { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }
		public bool? Enabled { get; set; }

		public virtual ICollection<Purchase> Purchases { get; set; }

		public Reseller SetRole()
		{
			Role = nameof(Reseller);
			return this;
		}

		public Reseller SetPassword()
		{
			Password = CryptographyExtensions.Sha256Hash(Password);
			return this;
		}

		public Reseller Clear()
		{
			if (!Cpf.IsNullOrWhiteSpace())
			{
				Cpf = Cpf.Replace(".", "").Replace(",", "").Replace("-", "");
			}

			return this;
		}
	}
}
