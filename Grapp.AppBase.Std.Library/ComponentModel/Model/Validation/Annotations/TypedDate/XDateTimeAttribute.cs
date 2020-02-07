using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Behaviors;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Enums;
using Grapp.AppBase.Std.Library.Properties;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedDate
{
	public class XDateTimeAttribute : XValidationAttribute, IDateTimeConstraint
	{
        public XDateTimeAttribute(bool useAdvancingCaret = true)
            : base(EDataType.DateTime, useAdvancingCaret ? EValidationMode.MaskDateTimeAdvancingCaret : EValidationMode.MaskDataTime)
        {
            ErrorMessage = DbMessages.XDateTimeAttribute_XDateTimeAttribute_O_campo__0__deve_ser_informado;
        }

        // ReSharper disable LocalizableElement
        public override string Mask => "G";
        // ReSharper restore LocalizableElement

        public EDateTimeFormat Format => EDateTimeFormat.DateTime;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                return ValidationResult.Success;
            }

            var propertyInfo = XHelper.Reflections.Properties.Get(validationContext.ObjectType, validationContext.MemberName);

            if (XHelper.Types.IsNullable(propertyInfo.PropertyType) && value == null)
            {
                return ValidationResult.Success;
            }

            if (DateTime.TryParse(value.ToString(), out var dateTime) && !XHelper.Models.DateTimeIsValid(dateTime))
            {
                return GetErrorResult(validationContext);
            }

            return ValidationResult.Success;
        }

        private ValidationResult GetErrorResult(ValidationContext validationContext) => new ValidationResult(String.Format(ErrorMessage, validationContext?.DisplayName));
        
        protected override bool InternalIsValid(object value) => true;
    }
}