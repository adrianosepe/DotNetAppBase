using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Attributes.ViewDesign
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class ViewAttribute : Attribute
	{
		protected ViewAttribute() { }

		public ViewAttribute(string id) => ID = Guid.Parse(id);

	    public Guid? ID { get; }
	}
}