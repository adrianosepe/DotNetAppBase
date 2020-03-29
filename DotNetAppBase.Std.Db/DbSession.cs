using System;
using System.Collections.Generic;
using System.Data.Common;
using DotNetAppBase.Std.Db.Contract;
using DotNetAppBase.Std.Db.Enums;
using DotNetAppBase.Std.Exceptions.Base;

namespace DotNetAppBase.Std.Db
{
    [Serializable]
    public abstract class DbSession : IDbSession
    {
        private readonly Dictionary<IDbAccess, DbContext> _accessInTransaction;

        private DbDatabase _database;

        private IDbTransactionManager _transactionManager;

        protected DbSession()
        {
            if(DbStorage.Instance.DefaultDatabase == null)
            {
                throw new XException("Não foi possível criar uma sessão apartir da base de dados Default.");
            }

            _database = DbStorage.Instance.DefaultDatabase as DbDatabase;
            if(_database == null)
            {
                throw new XException($"A classe {GetType().Name} não tem suporte a Database do tipo {DbStorage.Instance.DefaultDatabase.GetType().Name}");
            }

            _accessInTransaction = new Dictionary<IDbAccess, DbContext>();
        }

        protected DbSession(IDbDatabase database)
        {
            _database = database as DbDatabase;
            if(_database == null)
            {
                throw new XException($"A classe {GetType().Name} não tem suporte a Database do tipo {database.GetType().Name}");
            }

            _accessInTransaction = new Dictionary<IDbAccess, DbContext>();
        }

        public IDbDatabase Database
        {
            get => _database;
            protected set => _database = value as DbDatabase;
        }

        public bool InTransaction => TransactionManager.InTransaction;

        public IDbTransactionManager TransactionManager
        {
            get => _transactionManager ?? (_transactionManager = new DbTransactionManager(this));
            protected set => _transactionManager = value;
        }

        public static IDbSession NewFromDefault() => DbStorage.Instance.DefaultDatabase.NewSession();

        public void BeginTransaction()
        {
            TransactionManager.StartTransaction();
        }

        public IDbContext BuildContext() => InternalBuildContext();

        public void Commit()
        {
            TransactionManager.Commit();

            ChangeContextsState(EDbContextState.Confirmed);
        }

        public abstract DbDataAdapter CreateDataAtapter(DbCommand cmd);

        public abstract DbParameter CreateReturnParameter();

        public DbAccess GetAccess()
        {
            var context = BuildContext() as DbContext;
            var access = new DbAccess(this, context);

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

            ChangeContextsState(EDbContextState.Cancelled);
        }

        protected virtual IDbContext InternalBuildContext()
        {
            var context = TransactionManager.InTransaction
                              ? new DbContext(TransactionManager.Connection, TransactionManager.Transaction, false)
                              : new DbContext(_database.NewConnection(), true);

            return context;
        }

        internal void AddAccess(DbAccess access)
        {
            access.Session = this;
            if(access.Context.InTransaction)
            {
                _accessInTransaction.Add(access, (DbContext)access.Context);
            }
        }

        internal void RemoveAccess(IDbAccess dbAccess)
        {
            if(_accessInTransaction.ContainsKey(dbAccess))
            {
                _accessInTransaction.Remove(dbAccess);
            }
        }

        private void ChangeContextsState(EDbContextState state)
        {
            foreach(var context in _accessInTransaction.Values)
            {
                context.State = state;
            }
        }

        IDbAccess IDbAccessProvider.GetAccess() => GetAccess();
    }
}