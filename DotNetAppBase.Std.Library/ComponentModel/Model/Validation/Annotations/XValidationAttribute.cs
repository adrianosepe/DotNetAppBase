using System.ComponentModel;
using DotNetAppBase.Std.Library.ComponentModel.Model.Enums;

// ReSharper disable UnusedMember.Global

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations
{
    public abstract class XValidationAttribute : XDataTypeAttribute
    {
        private string _maskForEditor;

        protected XValidationAttribute(EDataType dataType, EValidationMode mode) : base(dataType) => Mode = mode;

        protected XValidationAttribute(EDataType dataType, EValidationMode mode, string errorMessage) 
            : base(dataType)
        {
            Mode = mode;

            ErrorMessage = errorMessage;
        }

        [Localizable(false)]
        public abstract string Mask { get; }

        public virtual bool Enabled => RestrictFor == EModoOperacao.None || RestrictFor == ValidationSettings.RestrictionFor;

        public string MaskForEditor
        {
            get => _maskForEditor ?? MaskWithoutStartAndEnd;
            set => _maskForEditor = value;
        }

        [Localizable(false)]
        public string MaskForEditorWithoutStartAndEnd => MaskForEditor.Replace("^", "").Replace("$", "");

        [Localizable(false)]
        public string MaskWithoutStartAndEnd => Mask.Replace("^", "").Replace("$", "");

        public EValidationMode Mode { get; protected set; }

        public EModoOperacao RestrictFor { get; set; }

        public sealed override bool IsValid(object value) => !Enabled || InternalIsValid(value);

        public enum EValidationMode
        {
            Custom,

            MaskDataTime,
            MaskDateTimeAdvancingCaret,
            MaskNumeric,
            MaskRegEx,
            MaskRegular,
            MaskSimple
        }
    }
}