using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedNumber
{
	public class XCurrencyAttribute : XNumberAttribute
	{
		public XCurrencyAttribute() : base(EDataType.Currency, EValidationMode.MaskNumeric) { }

		public override string Mask => "c";
	}
}