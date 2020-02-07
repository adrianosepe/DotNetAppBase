using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Types
{
	public class XTypeDescriptor<TModel> : XTypeDescriptor, IXTypeDescriptor<TModel>
	{
		protected XTypeDescriptor(Type type) : base(type) { }

		protected internal XTypeDescriptor() : this(typeof(TModel)) { }
	}
}