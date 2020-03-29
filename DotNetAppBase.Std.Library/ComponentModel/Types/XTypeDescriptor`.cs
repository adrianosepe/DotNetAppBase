using System;

namespace DotNetAppBase.Std.Library.ComponentModel.Types
{
	public class XTypeDescriptor<TModel> : XTypeDescriptor, IXTypeDescriptor<TModel>
	{
		protected XTypeDescriptor(Type type) : base(type) { }

		protected internal XTypeDescriptor() : this(typeof(TModel)) { }
	}
}