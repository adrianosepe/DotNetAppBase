using System.ComponentModel.DataAnnotations;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Svc;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation
{
    public static class EntityValidationResultExtensions
    {
		public static void Join(this EntityValidationResult @this, EntityValidationResult result)
        {
            if (result == null || result.IsEmpty)
            {
                return;
            }

            @this.InternalValidations.AddRange(result.InternalValidations);
        }

        public static void Join(this EntityValidationResult @this, ValidationResult validationResult)
        {
            if (validationResult != null)
            {
                @this.InternalValidations.Add(validationResult);
            }
        }
    }
}