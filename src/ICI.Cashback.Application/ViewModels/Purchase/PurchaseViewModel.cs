using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ICI.Cashback.Domain.Enums;

namespace ICI.Cashback.Application.ViewModels.Purchase
{
	public class PurchaseViewModel
	{
		[Key] public int Id { get; set; }

		[Display(Name = "Código")]
		[Required(ErrorMessage = "Código é obrigatório")]
		[MaxLength(30, ErrorMessage = "Máximo 30 caracteres")]
		public string Code { get; set; }

		[Display(Name = "Valor")]
		[Required(ErrorMessage = "Valor é obrigatório")]
		[DataType(DataType.Currency)]
		public float? Value { get; set; }

		[Display(Name = "Data")]
		[Required(ErrorMessage = "Data é obrigatória")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "dd/MM/yyyy", ApplyFormatInEditMode = true)]
		public DateTime? Date { get; set; }

		[Display(Name = "Revendedor")] public string ResellerCpf { get; set; }

		[Required(ErrorMessage = "Revendedor é obrigatório")]
		public int ResellerId { get; set; }

		public PurchaseStatus Status { get; set; } = PurchaseStatus.InValidation;

		public IEnumerable<string> Errors { get; set; }
	}
}
