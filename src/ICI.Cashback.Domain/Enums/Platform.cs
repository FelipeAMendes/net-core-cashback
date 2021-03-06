using System.ComponentModel.DataAnnotations;

namespace ICI.Cashback.Domain.Enums
{
	public enum Platform
	{
		[Display(Name = "WEB")] Web = 1,
		[Display(Name = "API")] Api
	}
}
