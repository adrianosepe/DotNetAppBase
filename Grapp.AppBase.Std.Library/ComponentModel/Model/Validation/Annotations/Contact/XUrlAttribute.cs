using System;
using System.Linq;
using System.Text.RegularExpressions;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Validation;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Contact
{
	public sealed class XUrlAttribute : XRegexAttribute
	{
		private const string Message = "O campo {0} não possui uma URL válida.";

		private static readonly Regex RegexReqProtocol =
			new Regex(
				ValidationDataFormats.ContactUrlReqProtocol, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		private static readonly Regex RegexOpProtocol =
			new Regex(
				ValidationDataFormats.ContactUrlOpProtocol, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		private static readonly Regex RegexWithoutProtocol =
			new Regex(
				ValidationDataFormats.ContactUrlWithoutProtocol, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		public XUrlAttribute(EUrlOption option = EUrlOption.RequireProtocol) : base(Translate(option), Message, EDataType.Url)
		{
			Mode = EValidationMode.MaskRegEx;

			MaskForEditor = ValidationDataFormats.ContactUrlMask;
		}

		private static Regex Translate(EUrlOption option)
		{
			switch(option)
			{
				case EUrlOption.RequireProtocol:
					return RegexReqProtocol;

				case EUrlOption.OptionalProtocol:
					return RegexOpProtocol;

				case EUrlOption.WithoutProtocol:
					return RegexWithoutProtocol;

				default:
					throw new ArgumentOutOfRangeException(nameof(option), option, null);
			}
		}
	}
}