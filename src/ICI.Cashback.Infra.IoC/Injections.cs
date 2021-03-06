using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ICI.Cashback.Application.AutoMapper;
using ICI.Cashback.Application.DependencyInjection;
using ICI.Cashback.Domain;
using ICI.Cashback.Domain.DependencyInjection;
using ICI.Cashback.Infra.Data.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ICI.Cashback.Infra.IoC
{
	public static class Injections
	{
		public static IServiceCollection InjectCashBackDependencies(this IServiceCollection services)
		{
			services.InfraDataDependencies();
			services.DomainDependencies();
			services.ApplicationDependencies();
			return services;
		}

		public static IServiceCollection InjectApiCashbackHttpClient(this IServiceCollection services)
		{
			services.AddHttpClient(CashbackConfiguration.ApiCashback.Name, option =>
			{
				option.BaseAddress = new Uri(CashbackConfiguration.ApiCashback.Url);
				option.DefaultRequestHeaders.ConnectionClose = true;
				option.DefaultRequestHeaders.Accept.Clear();
				option.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			});

			return services;
		}

		public static IServiceCollection InjectAutoMapper(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(AutoMapperConfiguration));
			var mapperConfiguration = AutoMapperConfiguration.RegisterMappings();
			var imapper = mapperConfiguration.CreateMapper();
			services.AddSingleton(imapper);
			return services;
		}

		public static IServiceCollection InjectAuthentication(this IServiceCollection services)
		{
			var key = Encoding.ASCII.GetBytes(CashbackConfiguration.Jwt.Secret);
			services.AddAuthentication(a =>
			{
				a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(j =>
			{
				j.RequireHttpsMetadata = false;
				j.SaveToken = true;
				j.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
				j.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						context.Token = context.Request.Cookies[CashbackConfiguration.Jwt.CookieName];
						return Task.CompletedTask;
					}
				};
			});
			return services;
		}
	}
}
