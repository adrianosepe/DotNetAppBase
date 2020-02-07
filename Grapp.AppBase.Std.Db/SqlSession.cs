using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Grapp.AppBase.Std.Exceptions.Base;
using Grapp.ApplicationBase.Db.Contract;

namespace Grapp.ApplicationBase.Db
{
    [Serializable]
    public abstract class SqlSession : ISqlSession
    {
        private readonly Dictionary<ISqlAccess, SqlContext> _accessInTransaction;

        private SqlDatabase _database;

        private ISqlTransactionManager _transactionManager;

        protected SqlSession()
        {
            if(SqlStorage.Instance.DefaultDatabase == null)
            {
                throw new XException(message: "Não foi possível criar uma sessão apartir da base de dados Default.");
            }

            _database = SqlStorage.Instance.DefaultDatabase as SqlDatabase;
            if(_database == null)
            {
                throw new XException(message: $"A classe {GetType().Name} não tem suporte a Database do tipo {SqlStorage.Instance.DefaultDatabase.GetType().Name}");
            }

            _accessInTransaction = new Dictionary<ISqlAccess, SqlContext>();
        }

        protected SqlSession(ISqlDatabase database)
        {
            _database = database as SqlDatabase;
            if(_database == null)
            {
                throw new XException(message: $"A classe {GetType().Name} não tem suporte a Database do tipo {database.GetType().Name}");
            }

            _accessInTransaction = new Dictionary<ISqlAccess, SqlContext>();
        }

        public ISqlDatabase Database
        {
            get => _database;
            protected set => _database = value as SqlDatabase;
        }

        public bool InTransaction => TransactionManager.InTransaction;

        public ISqlTransactionManager TransactionManager
        {
            get => _transactionManager ?? (_transactionManager = new SqlTransactionManager(session: this));
            protected set => _transactionManager = value;
        }

        public static ISqlSession NewFromDefault() => SqlStorage.Instance.DefaultDatabase.NewSession();

        public void BeginTransaction()
        {
            TransactionManager.StartTransaction();
        }

        public ISqlContext BuildContext()
        {
            return InternalBuildContext();
        }

        public void Commit()
        {
            TransactionManager.Commit();

            ChangeContextsState(ESqlContextState.Confirmed);
        }

        public abstract DbDataAdapter CreateDataAtapter(DbCommand cmd);

        public abstract DbParameter CreateReturnParameter();

        public SqlAccess GetAccess()
        {
            var context = BuildContext() as SqlContext;
            var access = new SqlAccess(session: this, context);

            if(context != null && context.InTransaction)
            {
                _accessInTransaction.Add(access, context);
            }

            return access;
        }

        public abstract bool RetryInteractionOnDbExcepion(DbException exception);

        public void Rollback()
        {
            TransactionManager.Rollback();

            ChangeContextsState(ESqlContextState.Cancelled);
        }

        protected virtual ISqlContext InternalBuildContext()
        {
            var context = TransactionManager.InTransaction
                              ? new SqlContext(TransactionManager.Connection, TransactionManager.Transaction, allowConnectionDispose: false)
                              : new SqlContext(connection: _database.NewConnection(), allowConnectionDispose: true);

            return context;
        }

        internal void AddAccess(SqlAccess access)
        {
            access.Session = this;
            if(access.Context.InTransaction)
            {
                _accessInTransaction.Add(access, value: (SqlContext)access.Context);
            }
        }

        internal void RemoveAccess(ISqlAccess sqlAccess)
        {
            if(_accessInTransaction.ContainsKey(sqlAccess))
            {
                _accessInTransaction.Remove(sqlAccess);
            }
        }

        private void ChangeContextsState(ESqlContextState state)
        {
            foreach(var context in _accessInTransaction.Values)
            {
                context.State = state;
            }
        }

        ISqlAccess ISqlAccessProvider.GetAccess() => GetAccess();
    }
}