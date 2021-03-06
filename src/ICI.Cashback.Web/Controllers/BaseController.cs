using ICI.Cashback.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ICI.Cashback.Web.Controllers
{
	public class BaseController : Controller
	{
		public void ShowMessage(MessageType messageType, string message)
		{
			TempData.Remove(messageType.ToString());
			TempData.Add(messageType.ToString(), message);
		}

		public void ShowMessage(MessageType messageType, IEnumerable<string> messages)
		{
			TempData.Remove(messageType.ToString());
			TempData.Add(messageType.ToString(), messages);
		}
	}
}
