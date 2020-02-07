using System;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Collection
{
	public interface IRefreshableDataSource
	{
		bool Refresh();
	}
}