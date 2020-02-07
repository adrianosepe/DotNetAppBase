using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Collection.Events
{
	public enum EEntityAction
	{
		NotInformed,

		Added,
		Updated,
		Removed,

		Canceled,
		Processed
	}
}