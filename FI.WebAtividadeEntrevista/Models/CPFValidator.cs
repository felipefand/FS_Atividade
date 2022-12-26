using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Linq;

namespace WebAtividadeEntrevista.Models
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class CPFValidator : ValidationAttribute
    {
		static Regex rx = new Regex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$");

		public override bool IsValid(object value)
        {
			if (value == null) return false;

			var cpf = value.ToString().Trim();

			if (!rx.IsMatch(cpf)) return false;

			cpf = cpf.Replace(".", "").Replace("-", "");
			var cpfDigits = cpf.ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();

			if (cpfDigits.GroupBy(x => x).Count() == 1) return false;

			var sum = cpfDigits.Take(9).Select((x, i) => x * (10 - i)).Sum();
			var digit = sum % 11;
			if (digit < 2)
				digit = 0;
			else
				digit = 11 - digit;

			if (digit != cpfDigits[9]) return false;

			sum = cpfDigits.Take(10).Select((x, i) => x * (11 - i)).Sum();
			digit = sum % 11;
			if (digit < 2)
				digit = 0;
			else
				digit = 11 - digit;

			if (digit != cpfDigits[10]) return false;

			return true;
        }
    }
}