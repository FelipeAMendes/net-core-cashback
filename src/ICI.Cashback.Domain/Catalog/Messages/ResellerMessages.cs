namespace ICI.Cashback.Domain.Catalog.Messages
{
	public class ResellerMessages
	{
		public const string SuccessRegister = "Cadastro efetuado com sucesso.";
		public const string SuccessEdit = "Cadastro alterado com sucesso.";
		public const string SuccessDelete = "Cadastro removido com sucesso.";

		public const string ErrorNameIsntProvided = "Nome Completo é obrigatório.";
		public const string ErrorCpfIsntProvided = "CPF é obrigatório.";
		public const string ErrorCpfInvalid = "CPF informado é inválido.";
		public const string ErrorExistingCpf = "CPF já cadastrado.";
		public const string ErrorEmailIsntProvided = "E-mail é obrigatório.";
		public const string ErrorEmailInvalid = "E-mail informado é inválido.";
		public const string ErrorExistingEmail = "E-mail já cadastrado.";
		public const string ErrorPasswordIsntProvided = "Senha é obrigatória.";
	}
}
