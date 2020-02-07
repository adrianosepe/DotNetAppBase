using System;
using System.Linq;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedString;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.PoS
{
	public class XPrintNameAttribute : XMaxLengthAttribute
	{
		public XPrintNameAttribute() : base(9) { }
	}
}