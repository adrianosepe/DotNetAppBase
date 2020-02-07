using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Grapp.ApplicationBase.Db
{
	public partial class SqlAccess
	{
        [Localizable(isLocalizable: false)]
		public static class Actions
		{
			public static T ExecuteProc<T>(string procName, params SqlParameter[] parameters)
			{
				using(var access = new SqlAccess())
				{
					return access.ExecProc<T, SqlParameter>(procName, parameters);
				}
			}

			public static int ExecuteProc(string procName, params SqlParameter[] parameters)
			{
				using(var access = new SqlAccess())
				{
					return access.ExecProc(procName, parameters);
				}
			}

			public static T ExecuteProc<T>(SqlSession session, string procName, params SqlParameter[] parameters)
			{
				using(var access = new SqlAccess())
				{
					access.Session = session;
					return access.ExecProc<T, SqlParameter>(procName, parameters);
				}
			}

			public static void ExecuteProcAndFill(DataTable dataTable, string procName, params SqlParameter[] parameters)
			{
				using(var access = new SqlAccess())
				{
					access.ExecProcAndFill(dataTable, procName, parameters);
				}
			}

			public static void ExecuteProcAndFill(SqlSession session, DataTable dataTable, string procName, params SqlParameter[] parameters)
			{
				using(var access = new SqlAccess())
				{
					access.Session = session;
					access.ExecProcAndFill(dataTable, procName, parameters);
				}
			}

		    public static int ExecuteText(string sql, params SqlParameter[] parameters)
		    {
		        using(var access = new SqlAccess())
		        {
		            return access.ExecText(sql, parameters);
		        }
		    }

			public static T ExecuteText<T>(string sql, params SqlParameter[] parameters)
			{
				return ExecuteText<T>(sql, CommandBehavior.Default, parameters);
			}

			public static T ExecuteText<T>(string sql, CommandBehavior behavior, params SqlParameter[] parameters)
			{
				using(var access = new SqlAccess())
				{
					return access.ExecText<T, SqlParameter>(sql, behavior, parameters);
				}
			}

			public static T ExecuteText<T>(SqlSession session, string sql, params SqlParameter[] parameters)
			{
				return ExecuteText<T>(session, sql, CommandBehavior.Default, parameters);
			}

			public static T ExecuteText<T>(SqlSession session, string sql, CommandBehavior behavior, params SqlParameter[] parameters)
			{
				using(var access = new SqlAccess())
				{
					access.Session = session;
					return access.ExecText<T, SqlParameter>(sql, behavior, parameters);
				}
			}
		}
	}
}