using System;
using System.Linq;
using Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedString;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Geo
{
	public class XLogradouroEnderecoAttribute : XMaxLengthAttribute
	{
		public XLogradouroEnderecoAttribute() : base(60) { }
	}
}