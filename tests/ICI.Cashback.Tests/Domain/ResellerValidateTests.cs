using System.Collections.Generic;
using System.Threading.Tasks;
using ICI.Cashback.Domain.Catalog.Messages;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Interfaces.Services;
using ICI.Cashback.Domain.Results;
using Moq;
using Xunit;

namespace ICI.Cashback.Tests.Domain
{
	public class ResellerValidateTests
	{
		[Fact]
		public void Reseller_Cpf_Isnt_Provided_Error()
		{
			var resellerModel = new Reseller
			{
				Email = "teste@teste.com",
				Name = "João da Silva",
				Password = "123456"
				//Cpf = "153.509.460-56"
			};

			var resellerService = new Mock<IResellerService>();
			resellerService.Setup(r => r.Add(resellerModel))
				.Returns(Task.FromResult(
					Result<IEnumerable<string>>.Create(false, new List<string> { ResellerMessages.ErrorCpfIsntProvided })));

			var result = resellerService.Object.Add(resellerModel).Result;
			Assert.False(result.Success);
			Assert.Equal(new List<string> { ResellerMessages.ErrorCpfIsntProvided },
				result.Object);
		}

		[Fact]
		public void Reseller_Name_Isnt_Provided_Error()
		{
			var resellerModel = new Reseller
			{
				Email = "teste@teste.com",
				//Name = "João da Silva",
				Password = "123456",
				Cpf = "153.509.460-56"
			};

			var resellerService = new Mock<IResellerService>();
			resellerService.Setup(r => r.Add(resellerModel))
				.Returns(Task.FromResult(
					Result<IEnumerable<string>>.Create(false, new List<string> { ResellerMessages.ErrorNameIsntProvided })));

			var result = resellerService.Object.Add(resellerModel).Result;
			Assert.False(result.Success);
			Assert.Equal(new List<string> { ResellerMessages.ErrorNameIsntProvided },
				result.Object);
		}

		[Fact]
		public void Reseller_Email_Isnt_Provided_Error()
		{
			var resellerModel = new Reseller
			{
				Email = "teste@teste.com",
				//Name = "João da Silva",
				Password = "123456",
				Cpf = "153.509.460-56"
			};

			var resellerService = new Mock<IResellerService>();
			resellerService.Setup(r => r.Add(resellerModel))
				.Returns(Task.FromResult(
					Result<IEnumerable<string>>.Create(false, new List<string> { ResellerMessages.ErrorEmailIsntProvided })));

			var result = resellerService.Object.Add(resellerModel).Result;
			Assert.False(result.Success);
			Assert.Equal(new List<string> { ResellerMessages.ErrorEmailIsntProvided },
				result.Object);
		}

		[Fact]
		public void Reseller_Cpf_Is_Valid_Error()
		{
			var resellerModel = new Reseller
			{
				Email = "teste@teste.com",
				Name = "João da Silva",
				Password = "123456",
				Cpf = "153.509.460-55" //Inválido
			};

			var resellerService = new Mock<IResellerService>();
			resellerService.Setup(r => r.Add(resellerModel))
				.Returns(Task.FromResult(
					Result<IEnumerable<string>>.Create(false, new List<string> { ResellerMessages.ErrorCpfInvalid })));

			var result = resellerService.Object.Add(resellerModel).Result;
			Assert.False(result.Success);
			Assert.Equal(new List<string> { ResellerMessages.ErrorCpfInvalid },
				result.Object);
		}

		[Fact]
		public void Reseller_Email_Is_Valid_Error()
		{
			var resellerModel = new Reseller
			{
				Email = "teste.com", //Inválido
				Name = "João da Silva",
				Password = "123456",
				Cpf = "153.509.460-56"
			};

			var resellerService = new Mock<IResellerService>();
			resellerService.Setup(r => r.Add(resellerModel))
				.Returns(Task.FromResult(
					Result<IEnumerable<string>>.Create(false, new List<string> { ResellerMessages.ErrorEmailInvalid })));

			var result = resellerService.Object.Add(resellerModel).Result;
			Assert.False(result.Success);
			Assert.Equal(new List<string> { ResellerMessages.ErrorEmailInvalid },
				result.Object);
		}

		[Fact]
		public void Reseller_Cpf_Existing_Error()
		{
			var resellerModel = new Reseller
			{
				Email = "teste@teste.com", //Existente
				Name = "João da Silva",
				Password = "123456",
				Cpf = "153.509.460-56"
			};

			var resellerService = new Mock<IResellerService>();
			resellerService.Setup(r => r.Add(resellerModel))
				.Returns(Task.FromResult(
					Result<IEnumerable<string>>.Create(false, new List<string> { ResellerMessages.ErrorExistingCpf })));

			var result = resellerService.Object.Add(resellerModel).Result;
			Assert.False(result.Success);
			Assert.Equal(new List<string> { ResellerMessages.ErrorExistingCpf },
				result.Object);
		}

		[Fact]
		public void Reseller_Email_Existing_Error()
		{
			var resellerModel = new Reseller
			{
				Email = "testes@teste.com",
				Name = "João da Silva",
				Password = "123456",
				Cpf = "153.509.460-56" //Existente
			};

			var resellerService = new Mock<IResellerService>();
			resellerService.Setup(r => r.Add(resellerModel))
				.Returns(Task.FromResult(
					Result<IEnumerable<string>>.Create(false, new List<string> { ResellerMessages.ErrorExistingEmail })));

			var result = resellerService.Object.Add(resellerModel).Result;
			Assert.False(result.Success);
			Assert.Equal(new List<string> { ResellerMessages.ErrorExistingEmail },
				result.Object);
		}
	}
}
