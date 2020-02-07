using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Linq;
using Grapp.AppBase.Std.Exceptions.Assert;
using Grapp.AppBase.Std.Exceptions.Base;
using Grapp.AppBase.Std.Library;
using Grapp.ApplicationBase.Db.Contract;
using Grapp.ApplicationBase.Db.SqlTrace;

namespace Grapp.ApplicationBase.Db
{
    public partial class SqlAccess : ISqlAccess
    {
        private readonly List<IDisposable> _disposableObjects = new List<IDisposable>();

        public EventHandler Disposed;
        private bool _disposed;

        private ISqlSession _session;

        public SqlAccess()
        {
            if(SqlStorage.Instance.DefaultDatabase == null)
            {
                throw new XException(message: "Não foi possível recuperar a base de dados default.");
            }

            if(SqlStorage.Instance.DefaultDatabase.DefaultSession is SqlSession session)
            {
                session.AddAccess(access: this);
            }
            else
            {
                Session = SqlStorage.Instance.DefaultDatabase.DefaultSession;
            }
        }

        public SqlAccess(ISqlSession session) : this(session, context: session.BuildContext()) { }

        public SqlAccess(ISqlSession session, ISqlContext context)
        {
            _session = session;

            Context = context;
        }

        public object Calller { get; set; }

        public int? CommandTimeout { get; set; }

        public ISqlContext Context { get; private set; }

        public ISqlSession Session
        {
            get => _session;
            internal set
            {
                if(_session == value)
                {
                    return;
                }

                _session = value;
                Context = _session.BuildContext();
            }
        }

        public ESqlContextState TransactionState => Context.State;

        public TResult ExecFunc<TResult, TParam>(string funcName, params TParam[] parameters) where TParam : DbParameter
        {
            try
            {
                return InternalExecuteFunc<TResult, TParam>(funcName, parameters);
            }
            catch(DbException ex)
            {
                if(_session.RetryInteractionOnDbExcepion(ex))
                {
                    return InternalExecuteFunc<TResult, TParam>(funcName, parameters);
                }

                XDebug.OnException(ex);

                throw;
            }
        }

        public TResult ExecProc<TResult, TParam>(string procName, params TParam[] parameters) where TParam : DbParameter
        {
            try
            {
                return InternalExecuteProc<TResult, TParam>(procName, parameters);
            }
            catch(SqlException ex)
            {
                if(_session.RetryInteractionOnDbExcepion(ex))
                {
                    return InternalExecuteProc<TResult, TParam>(procName, parameters);
                }

                XDebug.OnException(ex);

                throw;
            }
        }

        public int ExecProc<TParam>(string procName, params TParam[] parameters) where TParam : DbParameter
        {
            return ExecProc<Count, TParam>(procName, parameters);
        }

        public void ExecProcAndFill<TParam>(DataTable dataTable, string procName, params TParam[] parameters) where TParam : DbParameter
        {
            try
            {
                InternalExecuteProcAndFill(dataTable, procName, parameters);
            }
            catch(SqlException ex)
            {
                if(_session.RetryInteractionOnDbExcepion(ex))
                {
                    InternalExecuteProcAndFill(dataTable, procName, parameters);

                    return;
                }

                XDebug.OnException(ex);

                throw;
            }
        }

        public int ExecText<TParam>(string sql, params TParam[] parameters) where TParam : DbParameter
        {
            return ExecText<Count, TParam>(sql, parameters);
        }

        public TResult ExecText<TResult, TParam>(string sql, params TParam[] parameters) where TParam : DbParameter
        {
            return ExecText<TResult, TParam>(sql, CommandBehavior.Default, parameters);
        }

        public TResult ExecText<TResult, TParam>(string sql, CommandBehavior behavior, params TParam[] parameters) where TParam : DbParameter
        {
            try
            {
                return InternalExecuteText<TResult, TParam>(sql, parameters, behavior);
            }
            catch(SqlException ex)
            {
                if(_session.RetryInteractionOnDbExcepion(ex))
                {
                    return InternalExecuteText<TResult, TParam>(sql, parameters, behavior);
                }

                XDebug.OnException(ex);

                throw;
            }
        }

        public void OpenConnection()
        {
            Context.Open();
        }

        private static EReturnType GetReturnType(Type type)
        {
            if(type == typeof(DataSet))
            {
                return EReturnType.DataSet;
            }

            if(type == typeof(SqlDataReader))
            {
                return EReturnType.DataReader;
            }

            if(type == typeof(Count))
            {
                return EReturnType.Count;
            }

            if(type == typeof(DataTable))
            {
                return EReturnType.DataTable;
            }

            if(type == typeof(DataRow))
            {
                return EReturnType.DataRow;
            }

            if(type == typeof(Return))
            {
                return EReturnType.ProcReturn;
            }

            if(type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ReturnAndData<>))
            {
                return EReturnType.ProcReturnAndValue;
            }

            return EReturnType.Unknown;
        }

        private void AddDisposableObject(params IDisposable[] objects)
        {
            foreach(var obj in objects)
            {
                _disposableObjects.Add(obj);
            }
        }

        private static void AddParameters<TParam>(DbCommand command, TParam[] parameters) where TParam : DbParameter
        {
            if(parameters == null || parameters.Length == 0)
            {
                return;
            }

            foreach(var param in parameters)
            {
                if(param == null)
                {
                    continue;
                }

                command.Parameters.Add(param);
            }
        }

        private void ConfigCommandTimeout(DbCommand command)
        {
            command.CommandTimeout = CommandTimeout ?? Session.Database.CommandTimeout;
        }

        private TResult InternalExecuteProc<TResult, TParam>(string procName, TParam[] parameters) where TParam : DbParameter
        {
            OpenConnection();

            var command = Context.CreateCommand();
            command.CommandText = procName;
            command.CommandType = CommandType.StoredProcedure;

            ConfigCommandTimeout(command);

            AddParameters(command, parameters);

            try
            {
                SqlTraceProvider.Instance.Trace(access: this, command);

                var returnType = typeof(TResult);
                return (TResult)ExecuteCommand(returnType, command);
            }
            finally
            {
                command.Parameters.Clear();
            }
        }

        private TResult InternalExecuteFunc<TResult, TParam>(string funcName, TParam[] parameters) where TParam : DbParameter
        {
            var returnParameter = _session.CreateReturnParameter().CastTo<TParam>();

            InternalExecuteProc<Count, TParam>(funcName, parameters: parameters.Union(second: new[] {returnParameter}).ToArray());

            return XHelper.Sql.GetFromDbValue<TResult>(returnParameter.Value);
        }

        private void InternalExecuteProcAndFill<TParam>(DataTable dataTable, string procName, TParam[] parameters) where TParam : DbParameter
        {
            OpenConnection();

            var cmd = Context.CreateCommand();
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;

            ConfigCommandTimeout(cmd);

            AddParameters(cmd, parameters);
            try
            {
                SqlTraceProvider.Instance.Trace(access: this, cmd);

                using(var da = _session.CreateDataAtapter(cmd))
                {
                    da.Fill(dataTable);
                }
            }
            finally
            {
                cmd.Parameters.Clear();
            }
        }

        private TResult InternalExecuteText<TResult, TParam>(string sql, TParam[] parameters, CommandBehavior behavior) where TParam : DbParameter
        {
            OpenConnection();

            var command = Context.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;

            ConfigCommandTimeout(command);

            AddParameters(command, parameters);

            try
            {
                SqlTraceProvider.Instance.Trace(access: this, command);

                var returnType = typeof(TResult);
                return (TResult)ExecuteCommand(returnType, command, behavior);
            }
            finally
            {
                command.Parameters.Clear();
            }
        }

        private object ExecuteCommand(Type returnType, DbCommand command, CommandBehavior behavior = CommandBehavior.Default)
        {
            var enumReturnType = GetReturnType(returnType);

            switch(enumReturnType)
            {
                case EReturnType.DataReader:
                {
                    var reader = command.ExecuteReader(behavior);
                    AddDisposableObject(command, reader);

                    return reader;
                }

                case EReturnType.DataSet:
                {
                    using(var da = _session.CreateDataAtapter(command))
                    {
                        var ds = new DataSet();
                        da.Fill(ds);

                        return ds;
                    }
                }

                case EReturnType.DataTable:
                {
                    using(var da = _session.CreateDataAtapter(command))
                    {
                        var ds = new DataSet();
                        da.Fill(ds);

                        return ds.Tables[index: 0];
                    }
                }

                case EReturnType.DataRow:
                {
                    using(var da = _session.CreateDataAtapter(command))
                    {
                        var ds = new DataSet();
                        da.Fill(ds);

                        return ds.Tables[index: 0].Rows.Count == 0 ? null : ds.Tables[index: 0].Rows[index: 0];
                    }
                }

                case EReturnType.Count:
                {
                    return (Count)command.ExecuteNonQuery();
                }

                case EReturnType.ProcReturn:
                {
                    var returnParameter = _session.CreateReturnParameter();
                    command.Parameters.Add(returnParameter);

                    command.ExecuteNonQuery();

                    return Activator.CreateInstance(returnType, returnParameter.Value);
                }

                // Na teoria isso não funciona!
                case EReturnType.ProcReturnAndValue:
                {
                    var secondDataType = returnType.GetGenericArguments()[0];

                    var returnParameter = _session.CreateReturnParameter();
                    command.Parameters.Add(returnParameter);

                    var returnData = ExecuteCommand(secondDataType, command);

                    return Activator.CreateInstance(returnType, returnParameter.Value, returnData);
                }

                default:
                {
                    var data = command.ExecuteScalar();

                    if(returnType != typeof(object))
                    {
                        data = Convert.ChangeType(data, returnType);
                    }

                    return data;
                }
            }
        }

        #region Dispose Handle

        public void Dispose()
        {
            Dispose(disposing: true);
        }

        private void Dispose(bool disposing)
        {
            if(_disposed)
            {
                return;
            }

            _disposed = true;

            PartialDispose(disposing);

            if(disposing)
            {
                GC.SuppressFinalize(obj: this);
            }

            if(Context is SqlContext context)
            {
                context.Dispose(disposing);
            }
            else
            {
                Context?.Dispose();
            }

            var session = _session as SqlSession;
            session?.RemoveAccess(sqlAccess: this);

            Disposed?.Invoke(sender: this, EventArgs.Empty);
        }

        public void PartialDispose()
        {
            PartialDispose(disposing: true);
        }

        public void PartialDispose(bool disposing)
        {
            foreach(var disposable in _disposableObjects.Where(disposable => !(disposable is DbDataReader) || disposing))
            {
                disposable.Dispose();
            }

            _disposableObjects.Clear();
        }

        ~SqlAccess()
        {
            Dispose(disposing: false);
        }

        #endregion Dispose Handle
    }
}