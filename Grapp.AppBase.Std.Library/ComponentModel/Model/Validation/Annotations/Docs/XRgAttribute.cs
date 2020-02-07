using System;
using System.Linq;
using System.Text.RegularExpressions;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Validation;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Docs
{
	public class XRgAttribute : XRegexAttribute
	{
		private const string Message = "O campo {0} não possui um RG válido.";

		private static readonly Regex Regex =
			new Regex(
				ValidationDataFormats.DocRgRegex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		public XRgAttribute() : base(Regex, Message)
		{
			Mode = EValidationMode.MaskSimple;
			MaskForEditor = ValidationDataFormats.DocRgMask;
		}

	    public override bool Enabled => !ValidationSettings.InParaguayMode;
	}
}