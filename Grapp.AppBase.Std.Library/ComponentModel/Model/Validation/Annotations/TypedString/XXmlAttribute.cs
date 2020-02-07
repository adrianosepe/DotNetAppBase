using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedString
{
	public class XXmlAttribute : XMaxLengthAttribute
	{
		public XXmlAttribute() : base(EDataType.Xml, Int32.MaxValue) { }
	}
}