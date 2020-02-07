using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Enums;
using Grapp.AppBase.Std.Library.Properties;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    public class XRequiredAttribute : ValidationAttribute
    {
        public XRequiredAttribute() => ErrorMessage = DbMessages.XRequiredAttribute_XRequiredAttribute_O_campo__0__deve_ser_informado_;

        public EDataType DataType { get; set; }

        public virtual bool Enabled => RestrictFor == EModoOperacao.None || RestrictFor == ValidationSettings.RestrictionFor;

        public EModoOperacao RestrictFor { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(!Enabled)
            {
                return ValidationResult.Success;
            }

            if(value == null)
            {
                return GetErrorResult(validationContext);
            }

            if(value.GetType().IsArray && ((Array)value).Length == 0)
            {
                return GetErrorResult(validationContext);
            }

            if(value.GetType().IsEnum && !XHelper.Enums.IsDefined((Enum)value))
            {
                return GetErrorResult(validationContext);
            }

            if(value is IEnumerable valueenum && !valueenum.GetEnumerator().MoveNext())
            {
                return GetErrorResult(validationContext);
            }

            switch(Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.Int32:
                    if(DataType == EDataType.ZeroIsInvalid)
                    {
                        if(Convert.ToInt32(value) == 0)
                        {
                            return GetErrorResult(validationContext);
                        }
                    }

                    break;

                case TypeCode.String:
                    if(String.IsNullOrEmpty(value as string))
                    {
                        return GetErrorResult(validationContext);
                    }

                    break;
            }

            return ValidationResult.Success;
        }

        private ValidationResult GetErrorResult(ValidationContext validationContext) => new ValidationResult(String.Format(ErrorMessage, validationContext?.DisplayName));

        public enum EDataType
        {
            Common,
            ZeroIsInvalid
        }
    }
}