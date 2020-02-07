using System;
using System.Linq;
using System.Text.RegularExpressions;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Validation;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Geo
{
	public class XLogradouroNumeroAttribute : XRegexAttribute
	{
		private const string Message = "O campo {0} não possui um Número de Logradouro válido.";

		private static readonly Regex Regex =
			new Regex(
				ValidationDataFormats.GeoEnderecoNumeroRegex,
				RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		public XLogradouroNumeroAttribute() : base(Regex, Message, EDataType.Custom) { }
	}
}