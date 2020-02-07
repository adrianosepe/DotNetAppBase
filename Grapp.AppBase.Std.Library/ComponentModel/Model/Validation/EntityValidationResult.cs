using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation
{
	[DataContract, Serializable]
	public class EntityValidationResult
	{
		public static readonly EntityValidationResult Empty = new EntityValidationResult();

		internal readonly List<ValidationResult> InternalValidations;

	    public EntityValidationResult(params string[] errors) : this(errors.Select(error => new ValidationResult(error))) { }

		public EntityValidationResult(params IEnumerable<ValidationResult>[] validations)
		{
		    InternalValidations = validations
		        .SelectMany(
		            result =>
		                {
		                    var validationResults = result as ValidationResult[] ?? result.ToArray();

		                    return validationResults.ToList();
		                })
		        .ToList();
		}

		public EntityValidationResult(IEnumerable<ValidationResult> validations = null)
		{
			InternalValidations = validations?.ToList() ?? new List<ValidationResult>();
		}

		[DataMember]
		public bool AnyViolationOrWarning => HasViolations || HasWarning;

		[DataMember]
		public bool HasViolations => Validations.Any(result => !(result is WarningValidationResult));

		[DataMember]
		public bool HasWarning => Validations.Any(result => result is WarningValidationResult);

		[DataMember]
		public bool IsEmpty => InternalValidations.Count == 0;

		[DataMember]
		public IEnumerable<ValidationResult> Validations => InternalValidations;

        // ReSharper disable LocalizableElement
        public override string ToString() => Validations.Select(result => result.ErrorMessage).Aggregate((s, s1) => s + "\n" + s1);
		// ReSharper restore LocalizableElement
	}
}