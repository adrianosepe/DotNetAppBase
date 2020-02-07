using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using Grapp.AppBase.Std.Exceptions.Base;
using Grapp.ApplicationBase.Db.Contract;

namespace Grapp.ApplicationBase.Db
{
    public class SqlContext : ISqlContext
    {
        private readonly bool _allowConnectionDispose;

        private bool _disposed;

        public SqlContext(DbConnection connection, bool allowConnectionDispose)
        {
            Connection = connection;
            State = ESqlContextState.OutTransaction;

            _allowConnectionDispose = allowConnectionDispose;
        }

        public SqlContext(DbConnection connection, DbTransaction transaction, bool allowConnectionDispose) : this(connection, allowConnectionDispose)
        {
            Transaction = transaction;
            State = ESqlContextState.InTransaction;

            _allowConnectionDispose = allowConnectionDispose;
        }

        public DbConnection Connection { get; private set; }

        public bool InTransaction => Transaction != null;

        public bool IsAvailable => State != ESqlContextState.Disposed && (State == ESqlContextState.OutTransaction || State == ESqlContextState.InTransaction);

        public ESqlContextState State { get; internal set; }

        public DbTransaction Transaction { get; }

        ~SqlContext()
        {
            Dispose(disposing: false);
        }

        public void Close()
        {
            if(!InTransaction)
            {
                Connection.Close();
            }
        }

        public DbCommand CreateCommand()
        {
            ThrowExceptionIsNotAvailable();

            var command = Connection.CreateCommand();
            command.Transaction = Transaction;

            return command;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
        }

        public void Open()
        {
            ThrowExceptionIsNotAvailable();

            if(Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
        }

        internal void Dispose(bool disposing)
        {
            if(_disposed)
            {
                return;
            }

            _disposed = true;

            if(!InTransaction)
            {
                if(disposing && _allowConnectionDispose)
                {
                    Connection.Dispose();
                }

                Connection = null;
            }

            State = ESqlContextState.Disposed;

            if(disposing)
            {
                GC.SuppressFinalize(obj: this);
            }
        }

        private void ThrowExceptionIsNotAvailable()
        {
            if(!IsAvailable)
            {
                throw new XException(message: "A contexto de transação foi finalizado tornando-se indisponível para essa operação.");
            }
        }
    }
}