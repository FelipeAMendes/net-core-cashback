using System.ComponentModel.DataAnnotations;

namespace ICI.Cashback.Domain.Enums
{
	public enum PurchaseStatus
	{
		[Display(Name = "Em Validação")] InValidation = 1,
		[Display(Name = "Aprovado")] Approved
	}
}
