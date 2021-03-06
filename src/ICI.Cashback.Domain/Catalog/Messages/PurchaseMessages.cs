namespace ICI.Cashback.Domain.Catalog.Messages
{
	public class PurchaseMessages
	{
		public const string SuccessRegister = "Cadastro efetuado com sucesso.";
		public const string SuccessEdit = "Cadastro alterado com sucesso.";
		public const string SuccessDelete = "Cadastro removido com sucesso.";

		public const string ErrorCodeIsntProvided = "Código é obrigatório.";
		public const string ErrorDateIsntProvided = "Data é obrigatória.";
		public const string ErrorResellerIdIsntProvided = "Revendedor é obrigatório.";
		public const string ErrorValueIsntProvided = "Valor é obrigatória.";
	}
}
