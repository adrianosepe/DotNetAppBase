using System;
using System.Linq;
using System.Text.RegularExpressions;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Validation;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Contact
{
	public sealed class XEmailAttribute : XRegexAttribute
	{
		private const string Message = "O campo {0} não possui um e-mail válido.";

		private static readonly Regex Regex =
			new Regex(
				ValidationDataFormats.ContactEmailRegex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		public XEmailAttribute() : base(Regex, Message, EDataType.EmailAddress)
		{
			MaskForEditor = ValidationDataFormats.ContactEmailMask;
		}

        protected override bool InternalIsValid(object value)
        {
            var result = base.InternalIsValid(value);
            if (!result)
            {
                return false;
            }

            return (value?.ToString().Length ?? 0) < 80;
        }
    }
}