using System;
using System.Linq;
using System.Text.RegularExpressions;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Validation;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Bussines
{
	public sealed class XNcmAttribute : XRegexAttribute
	{
		private const string Message = "O campo {0} não possui um NCM válido.";

		private static readonly Regex Regex =
			new Regex(
				ValidationDataFormats.BussinesNcmRegex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		public XNcmAttribute() : base(Regex, Message)
		{
			Mode = EValidationMode.MaskSimple;
			MaskForEditor = ValidationDataFormats.BussinesNcmMask;
		}
	}
}