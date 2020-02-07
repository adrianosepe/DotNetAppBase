using System;
using System.Linq;

namespace Grapp.ApplicationBase.Db.SqlTrace
{
	public interface ISqlTraceViewerRegister : IDisposable
	{
		SqlTraceEventHandler SqlTraceHandler { get; }
	}
}