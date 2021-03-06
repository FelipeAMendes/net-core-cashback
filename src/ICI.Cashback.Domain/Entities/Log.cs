using System;

namespace ICI.Cashback.Domain.Entities
{
	public class Log : Entity
	{
		public DateTime Date { get; set; }
		public string UserIp { get; set; }
		public string Object { get; set; }
		public int OperationId { get; set; }
		public string User { get; set; }
		public string Table { get; set; }
		public string Platform { get; set; }
	}
}
