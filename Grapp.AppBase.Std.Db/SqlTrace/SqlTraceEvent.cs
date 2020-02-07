using System;
using System.Data.Common;

namespace Grapp.ApplicationBase.Db.SqlTrace
{
	public class SqlTraceEventArgs : EventArgs
	{
		public SqlTraceEventArgs(DbCommand command, string connectionString)
		{
			Command = command;
			ConnectionString = connectionString;
		}

		public DbCommand Command { get; }

		public string ConnectionString { get; }
	}

	public delegate void SqlTraceEventHandler(object sender, SqlTraceEventArgs e);
}