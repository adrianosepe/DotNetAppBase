using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Attributes
{
    public class EntityAssociateValidationResult : ValidationResult
    {
        public EntityAssociateValidationResult([Localizable(false)] string errorMessage, IEnumerable<ValidationResult> violations) : base(errorMessage)
        {
            Violations = violations;
        }

        public IEnumerable<ValidationResult> Violations { get; }
    }
}