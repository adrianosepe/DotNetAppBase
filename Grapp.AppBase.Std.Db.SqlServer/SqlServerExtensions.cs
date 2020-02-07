using System.ComponentModel;
using System.Data;
using Grapp.ApplicationBase.Db.Contract;
using Microsoft.Data.SqlClient;

[Localizable(isLocalizable: false)]
// ReSharper disable CheckNamespace
public static class SqlServerExtensions
// ReSharper restore CheckNamespace
{
    public static TResult ExecuteFunc<TResult>(this ISqlAccess access, string funcName, params SqlParameter[] parameters)
    {
        return access.ExecFunc<TResult, SqlParameter>(funcName, parameters);
    }

    public static T ExecuteProc<T>(this ISqlAccess access, [Localizable(isLocalizable: false)] string procName, params SqlParameter[] parameters)
    {
        return access.ExecProc<T, SqlParameter>(procName, parameters);
    }

    public static int ExecuteProc(this ISqlAccess access, string procName, params SqlParameter[] parameters)
    {
        return access.ExecProc(procName, parameters);
    }

    public static void ExecuteProcAndFill(this ISqlAccess access, DataTable dataTable, string procName, params SqlParameter[] parameters)
    {
        access.ExecProcAndFill(dataTable, procName, parameters);
    }

    public static TResult ExecuteText<TResult>(this ISqlAccess access, string sql, params SqlParameter[] parameters)
    {
        return access.ExecText<TResult, SqlParameter>(sql, parameters);
    }

    public static TResult ExecuteText<TResult>(this ISqlAccess access, string sql, CommandBehavior behavior, params SqlParameter[] parameters)
    {
        return access.ExecText<TResult, SqlParameter>(sql, behavior, parameters);
    }

    public static int ExecuteText(this ISqlAccess access, string sql, params SqlParameter[] parameters)
    {
        return access.ExecText(sql, parameters);
    }

    public static string RenameObjectToRunAsAutonomousTransaction(this ISqlAccess access, string schema, object objectName)
    {
        var builder = new SqlConnectionStringBuilder(access.Session.Database.ConnectionString);
        var initialCatalog = builder.InitialCatalog;

        return $"loopback.[{initialCatalog}].[{schema}].[{objectName}]";
    }
}