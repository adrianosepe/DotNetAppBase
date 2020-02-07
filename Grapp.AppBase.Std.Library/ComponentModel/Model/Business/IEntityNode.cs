using System;
using System.Collections.Generic;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Model.Business
{
	public interface IEntityNode
	{
		string Name { get; }

		IEntityNode Parent { get; }

		IEnumerable<IEntityNode> Children { get; }

		bool IsParent { get; }
	}
}