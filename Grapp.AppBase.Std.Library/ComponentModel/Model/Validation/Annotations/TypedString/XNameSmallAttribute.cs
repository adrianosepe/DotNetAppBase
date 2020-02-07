using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedString
{
	public class XNameSmallAttribute : XMaxLengthAttribute
	{
		public XNameSmallAttribute() : base(30) { }
	}
}