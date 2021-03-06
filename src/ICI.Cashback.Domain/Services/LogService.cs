using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using ICI.Cashback.Domain.Entities;
using ICI.Cashback.Domain.Enums;
using ICI.Cashback.Domain.Extensions;
using ICI.Cashback.Domain.Interfaces.Repositories;
using ICI.Cashback.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ICI.Cashback.Domain.Services
{
	public class LogService : ILogService
	{
		private readonly ILogRepository _logRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public LogService(ILogRepository logRepository, IHttpContextAccessor httpContextAccessor)
		{
			_logRepository = logRepository;
			_httpContextAccessor = httpContextAccessor;
		}

		public void Add<T>(OperationLog operationLog, T @object, Platform platform)
		{
			var entidade = CreateEntity(@object, operationLog, platform);
			_logRepository.Add(entidade);
		}

		#region Privates

		private Log CreateEntity<T>(T @object, OperationLog operationLog, Platform platform)
		{
			var objetoLog = new Log
			{
				Date = DateTime.Now,
				OperationId = (int) operationLog,
				Object = @object != null
					? JsonConvert.SerializeObject(@object, Formatting.Indented,
						new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore})
					: null,

				User = GetUser(),
				UserIp = GetIpAddress()
			};

			if (@object != null)
			{
				var type = @object.GetType();
				if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
					type = type.GetGenericArguments()[0];

				objetoLog.Table = type.Name;
			}

			objetoLog.Platform = platform.GetDisplayName();
			return objetoLog;
		}

		public static string GetIpAddress()
		{
			try
			{
				var hostname = Environment.MachineName;
				var host = Dns.GetHostEntry(hostname);
				var ipAddress = string.Empty;
				foreach (var ip in host.AddressList)
					if (ip.AddressFamily == AddressFamily.InterNetwork)
						ipAddress = Convert.ToString(ip);

				return ipAddress;
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}

		private string GetUser()
		{
			try
			{
				return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
					? _httpContextAccessor.HttpContext.User.Identity.Name
					: string.Empty;
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}
		#endregion
	}
}
