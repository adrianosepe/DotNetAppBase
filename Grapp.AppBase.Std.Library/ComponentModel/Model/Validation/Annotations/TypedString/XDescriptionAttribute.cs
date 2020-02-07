using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedString
{
	public class XDescriptionAttribute : XMaxLengthAttribute
	{
		public XDescriptionAttribute() : this(800) { }

		public XDescriptionAttribute(int maxLength) : base(EDataType.MultilineText, maxLength) { }
	}
}