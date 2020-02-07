using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Grapp.AppBase.Std.Library.Properties;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Collection
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
	public class XNotEmptyAttribute : ValidationAttribute
	{
		public XNotEmptyAttribute()
		{
			ErrorMessage = DbMessages.XNotEmptyAttribute_XNotEmptyAttribute_O_campo__0__não_pode_ficar_vazio_;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if(value == null)
			{
				return GetErrorResult(validationContext);
			}

			if(value.GetType().IsArray && ((Array)value).Length == 0)
			{
				return GetErrorResult(validationContext);
			}

		    if(value is IEnumerable valueenum && !valueenum.GetEnumerator().MoveNext())
			{
				return GetErrorResult(validationContext);
			}

			return ValidationResult.Success;
		}

		private ValidationResult GetErrorResult(ValidationContext validationContext)
		{
			return new ValidationResult(String.Format(ErrorMessage, validationContext?.DisplayName));
		}
	}
}