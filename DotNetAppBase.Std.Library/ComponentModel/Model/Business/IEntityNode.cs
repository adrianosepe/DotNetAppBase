using System.Collections.Generic;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Business
{
	public interface IEntityNode
	{
		string Name { get; }

		IEntityNode Parent { get; }

		IEnumerable<IEntityNode> Children { get; }

		bool IsParent { get; }
	}
}