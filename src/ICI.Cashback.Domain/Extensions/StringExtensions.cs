using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace ICI.Cashback.Domain.Extensions
{
	public static class StringExtensions
	{
		public static bool IsNullOrWhiteSpace(this string str)
		{
			return string.IsNullOrWhiteSpace(str);
		}

		public static bool IsValidEmail(this string emailaddress)
		{
			try
			{
				_ = new MailAddress(emailaddress);
				return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}

		public static List<string> ToListString(this string str)
		{
			return new List<string> {str};
		}

		public static string FormatCpf(this string cpf)
		{
			cpf = cpf.Trim();
			if (cpf.Length != 11) return cpf;
			cpf = cpf.Insert(9, "-");
			cpf = cpf.Insert(6, ".");
			cpf = cpf.Insert(3, ".");
			return cpf;
		}

		public static bool IsValidCpf(this string cpf)
		{
			var mult1 = new int[9] {10, 9, 8, 7, 6, 5, 4, 3, 2};
			var mult2 = new int[10] {11, 10, 9, 8, 7, 6, 5, 4, 3, 2};

			cpf = cpf.Trim().Replace(".", "").Replace("-", "");
			if (cpf.Length != 11) return false;

			for (var j = 0; j < 10; j++)
				if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
					return false;

			var tempCpf = cpf.Substring(0, 9);
			var sum = 0;

			for (var i = 0; i < 9; i++)
				sum += int.Parse(tempCpf[i].ToString()) * mult1[i];

			var rest = sum % 11;
			rest = rest < 2 ? 0 : 11 - rest;

			var digit = rest.ToString();
			tempCpf += digit;
			sum = 0;
			for (var i = 0; i < 10; i++)
				sum += int.Parse(tempCpf[i].ToString()) * mult2[i];

			rest = sum % 11;
			rest = rest < 2 ? 0 : 11 - rest;

			digit += rest.ToString();
			return cpf.EndsWith(digit);
		}
	}
}
