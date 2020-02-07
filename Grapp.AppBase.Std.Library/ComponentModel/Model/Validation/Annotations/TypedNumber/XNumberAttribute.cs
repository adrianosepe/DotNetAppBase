using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedNumber
{
	public class XNumberAttribute : XValidationAttribute
	{
		protected XNumberAttribute(EDataType dataType, EValidationMode mode)
			: base(dataType, mode) { }

		public XNumberAttribute()
			: this(EDataType.Custom, EValidationMode.MaskRegEx) { }

		public override string Mask => throw new NotImplementedException();

		public int Precision { get; set; }

        protected override bool InternalIsValid(object value) => true;
    }
}