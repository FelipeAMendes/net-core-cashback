using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ICI.Cashback.Application.ViewModels.Reseller
{
	public class ResellerViewModel
	{
		[Key] public int Id { get; set; }

		[Display(Name = "Nome Completo")]
		[Required(ErrorMessage = "Nome Completo é obrigatório")]
		[MinLength(3, ErrorMessage = "Mínimo 3 caracteres")]
		[MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
		public string Name { get; set; }

		[Display(Name = "CPF")]
		[Required(ErrorMessage = "CPF é obrigatório")]
		[StringLength(14, ErrorMessage = "CPF informado é inválido")]
		public string Cpf { get; set; }

		[Display(Name = "E-mail")]
		[DataType(DataType.EmailAddress, ErrorMessage = "E-mail inválido")]
		[Required(ErrorMessage = "E-mail é obrigatório")]
		[MinLength(3, ErrorMessage = "Mínimo 3 caracteres")]
		[MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
		public string Email { get; set; }

		[Display(Name = "Confirmação de E-mail")]
		[DataType(DataType.EmailAddress, ErrorMessage = "E-mail informado é inválido")]
		[Compare("Email")]
		public string EmailConfirmation { get; set; }

		[Display(Name = "Senha")]
		[Required(ErrorMessage = "Senha é obrigatória")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Confirmação de Senha")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Senha a Confirmação de Senha não conferem")]
		public string PasswordConfirmation { get; set; }

		public IEnumerable<string> Errors { get; set; }
	}
}
