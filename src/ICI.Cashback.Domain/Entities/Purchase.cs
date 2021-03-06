using System;
using ICI.Cashback.Domain.Enums;

namespace ICI.Cashback.Domain.Entities
{
	public class Purchase : Entity
	{
		public string Code { get; set; }
		public float Value { get; set; }
		public DateTime Date { get; set; }
		public int ResellerId { get; set; }
		public PurchaseStatus Status { get; set; }

		public virtual Reseller Reseller { get; set; }
	}
}
