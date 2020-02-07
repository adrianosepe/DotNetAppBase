using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedNumber
{
	public class XPercentAttribute : XValidationAttribute
	{
		public XPercentAttribute() : base(EDataType.Date, EValidationMode.MaskNumeric) { }

		public override string Mask => "P";

        protected override bool InternalIsValid(object value) => true;
    }
}