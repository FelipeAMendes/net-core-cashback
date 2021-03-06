using System.ComponentModel.DataAnnotations;

namespace ICI.Cashback.Application.ViewModels.Purchase
{
	public class PurchaseListViewModel
	{
		public int Id { get; set; }

		[Display(Name = "Código")] public float Code { get; set; }

		[Display(Name = "Valor")] public string Value { get; set; }

		[Display(Name = "Data")] public string Date { get; set; }

		[Display(Name = "Cashback (%)")] public string CashbackPercent { get; set; }

		[Display(Name = "Cashback")] public string CashbackValue { get; set; }

		[Display(Name = "Status")] public string Status { get; set; }

		[Display(Name = "Revendedor(a)")] public string ResellerName { get; set; }
	}
}
