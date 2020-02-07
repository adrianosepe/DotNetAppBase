using System;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Grapp.ApplicationBase.Db.Contract
{
    public interface ISqlAccess : IDisposable
    {
        object Calller { get; set; }

        int? CommandTimeout { get; set; }

        ISqlContext Context { get; }

        ISqlSession Session { get; }

        ESqlContextState TransactionState { get; }

        TResult ExecFunc<TResult, TParam>(string funcName, params TParam[] parameters) where TParam : DbParameter;

        TResult ExecProc<TResult, TParam>(string procName, params TParam[] parameters) where TParam : DbParameter;

        int ExecProc<TParam>(string procName, params TParam[] parameters) where TParam : DbParameter;

        void ExecProcAndFill<TParam>(DataTable dataTable, string procName, params TParam[] parameters) where TParam : DbParameter;

        int ExecText<TParam>(string sql, params TParam[] parameters) where TParam : DbParameter;

        TResult ExecText<TResult, TParam>(string sql, params TParam[] parameters) where TParam : DbParameter;

        TResult ExecText<TResult, TParam>(string sql, CommandBehavior behavior, params TParam[] parameters) where TParam : DbParameter;

        void OpenConnection();

        void PartialDispose();

        void PartialDispose(bool disposing);
    }
}