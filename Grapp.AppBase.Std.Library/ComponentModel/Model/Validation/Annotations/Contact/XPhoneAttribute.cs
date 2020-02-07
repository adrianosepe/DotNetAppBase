using System;
using System.Linq;
using System.Text.RegularExpressions;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Validation;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Contact
{
	public sealed class XPhoneAttribute : XRegexAttribute
	{
		private const string Message = "O campo {0} não possui um número de telefone válido.";

		private static readonly Regex Regex =
			new Regex(
				ValidationDataFormats.RegexTelefonePatternFull9Digits, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		public XPhoneAttribute() : base(Regex, Message, EDataType.PhoneNumber)
		{
			Mode = EValidationMode.MaskSimple;
			MaskForEditor = ValidationDataFormats.ContactFoneMask9Digits;
		}
	}
}