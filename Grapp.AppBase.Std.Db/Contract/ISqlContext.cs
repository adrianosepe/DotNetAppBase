using System;
using System.Data.Common;
using System.Linq;

namespace Grapp.ApplicationBase.Db.Contract
{
	public interface ISqlContext : IDisposable
	{
		DbConnection Connection { get; }

		bool InTransaction { get; }

		bool IsAvailable { get; }

		ESqlContextState State { get; }

		DbTransaction Transaction { get; }

		void Close();

		DbCommand CreateCommand();

		void Open();
	}
}