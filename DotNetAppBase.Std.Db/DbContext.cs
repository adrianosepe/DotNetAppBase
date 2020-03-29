using System;
using System.Data;
using System.Data.Common;
using DotNetAppBase.Std.Db.Contract;
using DotNetAppBase.Std.Db.Enums;
using DotNetAppBase.Std.Exceptions.Base;

namespace DotNetAppBase.Std.Db
{
    public class DbContext : IDbContext
    {
        private readonly bool _allowConnectionDispose;

        private bool _disposed;

        public DbContext(DbConnection connection, bool allowConnectionDispose)
        {
            Connection = connection;
            State = EDbContextState.OutTransaction;

            _allowConnectionDispose = allowConnectionDispose;
        }

        public DbContext(DbConnection connection, DbTransaction transaction, bool allowConnectionDispose) : this(connection, allowConnectionDispose)
        {
            Transaction = transaction;
            State = EDbContextState.InTransaction;

            _allowConnectionDispose = allowConnectionDispose;
        }

        public DbConnection Connection { get; private set; }

        public bool InTransaction => Transaction != null;

        public bool IsAvailable => State != EDbContextState.Disposed && (State == EDbContextState.OutTransaction || State == EDbContextState.InTransaction);

        public EDbContextState State { get; internal set; }

        public DbTransaction Transaction { get; }

        ~DbContext()
        {
            Dispose(false);
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
            Dispose(true);
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

            State = EDbContextState.Disposed;

            if(disposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        private void ThrowExceptionIsNotAvailable()
        {
            if(!IsAvailable)
            {
                throw new XException("A contexto de transação foi finalizado tornando-se indisponível para essa operação.");
            }
        }
    }
}