using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedNumber
{
	public class XDayAttribute : XRangeAttribute
	{
		public XDayAttribute() : base(1, 31) { }
	}
}