using System;

namespace DotNetAppBase.Std.Library.ComponentModel.Attributes.ViewDesign
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class ViewAttribute : Attribute
	{
		protected ViewAttribute() { }

		public ViewAttribute(string id) => ID = Guid.Parse(id);

	    public Guid? ID { get; }
	}
}