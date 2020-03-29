using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using DotNetAppBase.Std.Library.ComponentModel.Model.Business;
using DotNetAppBase.Std.Library.ComponentModel.Model.Present.Attributes;
using DotNetAppBase.Std.Library.ComponentModel.Model.Utilities;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Key;

namespace DotNetAppBase.Std.Library.ComponentModel.Model
{
    [DebuggerDisplay("{" + nameof(DisplaySmall) + "}")]
    public abstract class EntityBase : Entity, IEntity, IDataErrorInfo
    {
        [Display(Name = EntityBaseDisplayPattern.DefaultDisplayNameID), XPrimaryKey]
        public abstract int ID { get; set; }

        public virtual string Display => DisplaySmall;

        public virtual string DisplaySmall => $"{ID}";
        
        public virtual string DisplayFull => $"{ID}";

        [NotMapped]
        string IDataErrorInfo.Error
        {
            get
            {
                var validationResult = EntityValidator.Validate(this);

                return !validationResult.HasViolations ? null : validationResult.ToString();
            }
        }

        [NotMapped]
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                var validationResult = EntityValidator.ValidateProperty(this, columnName);

                return !validationResult.HasViolations ? null : validationResult.ToString();
            }
        }

        public new class Metadata
        {
            [Display(Name = EntityBaseDisplayPattern.DefaultDisplayNameID), XPrimaryKey]
            public int ID { get; set; }
        }

        public class MetadataWithLookup
        {
            [Display(Name = EntityBaseDisplayPattern.DefaultDisplayNameID), XPrimaryKey, LookupDisplay(0)]
            public int ID { get; set; }
        }
    }
}