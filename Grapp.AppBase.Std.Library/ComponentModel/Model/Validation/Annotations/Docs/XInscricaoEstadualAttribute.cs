using System;
using System.Linq;
using System.Text.RegularExpressions;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Validation;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Docs
{
	public class XInscricaoEstadualAttribute : XRegexAttribute
	{
		private const string Message = "O campo {0} não possui uma Inscrição Estadual válida.";

		private static readonly Regex Regex =
			new Regex(
				ValidationDataFormats.DocInscEstadualRegex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		public XInscricaoEstadualAttribute() : base(Regex, Message)
		{
			Mode = EValidationMode.MaskSimple;
			MaskForEditor = ValidationDataFormats.DocInscEstadualMask;
		}
	}
}