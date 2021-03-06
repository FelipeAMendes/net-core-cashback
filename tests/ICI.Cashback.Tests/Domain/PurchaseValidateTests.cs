using System;
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
	public class PurchaseValidateTests
	{
		[Fact]
		public void Purchase_Code_Isnt_Provided_Error()
		{
			var purchaseModel = new Purchase
			{
				//Code = "123456",
				Date = DateTime.Now,
				ResellerId = 1,
				Value = 1000
			};

			var purchaseService = new Mock<IPurchaseService>();
			purchaseService.Setup(r => r.Add(purchaseModel))
				.Returns(Task.FromResult(
					Result<IEnumerable<string>>.Create(false, new List<string> { PurchaseMessages.ErrorCodeIsntProvided })));

			var result = purchaseService.Object.Add(purchaseModel).Result;
			Assert.False(result.Success);
			Assert.Equal(new List<string> { PurchaseMessages.ErrorCodeIsntProvided },
				result.Object);
		}

		[Fact]
		public void Purchase_Date_Isnt_Provided_Error()
		{
			var purchaseModel = new Purchase
			{
				Code = "123456",
				//Date = DateTime.Now,
				ResellerId = 1,
				Value = 1000
			};

			var purchaseService = new Mock<IPurchaseService>();
			purchaseService.Setup(r => r.Add(purchaseModel))
				.Returns(Task.FromResult(
					Result<IEnumerable<string>>.Create(false, new List<string> { PurchaseMessages.ErrorDateIsntProvided })));

			var result = purchaseService.Object.Add(purchaseModel).Result;
			Assert.False(result.Success);
			Assert.Equal(new List<string> { PurchaseMessages.ErrorDateIsntProvided },
				result.Object);
		}

		[Fact]
		public void Purchase_ResellerId_Isnt_Provided_Error()
		{
			var purchaseModel = new Purchase
			{
				Code = "123456",
				Date = DateTime.Now,
				//ResellerId = 1,
				Value = 1000
			};

			var purchaseService = new Mock<IPurchaseService>();
			purchaseService.Setup(r => r.Add(purchaseModel))
				.Returns(Task.FromResult(
					Result<IEnumerable<string>>.Create(false, new List<string> { PurchaseMessages.ErrorResellerIdIsntProvided })));

			var result = purchaseService.Object.Add(purchaseModel).Result;
			Assert.False(result.Success);
			Assert.Equal(new List<string> { PurchaseMessages.ErrorResellerIdIsntProvided },
				result.Object);
		}

		[Fact]
		public void Purchase_Value_Isnt_Provided_Error()
		{
			var purchaseModel = new Purchase
			{
				Code = "123456",
				Date = DateTime.Now,
				ResellerId = 1,
				//Value = 1000
			};

			var purchaseService = new Mock<IPurchaseService>();
			purchaseService.Setup(r => r.Add(purchaseModel))
				.Returns(Task.FromResult(
					Result<IEnumerable<string>>.Create(false, new List<string> { PurchaseMessages.ErrorValueIsntProvided })));

			var result = purchaseService.Object.Add(purchaseModel).Result;
			Assert.False(result.Success);
			Assert.Equal(new List<string> { PurchaseMessages.ErrorValueIsntProvided },
				result.Object);
		}
	}
}
