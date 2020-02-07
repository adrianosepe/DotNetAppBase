using System;
using System.Data.Common;
using System.Linq;
using Grapp.AppBase.Std.Library;
using Grapp.ApplicationBase.Db.Contract;

namespace Grapp.ApplicationBase.Db
{
    public abstract class SqlDatabase : ISqlDatabase
    {
        private ISqlSession _defaultSession;

        protected SqlDatabase(string name, int commandTimeout = 60)
        {
            Name = name;

            CommandTimeout = commandTimeout;
        }

        public abstract int ConnectionTimeout { get; set; }

        public int CommandTimeout { get; set; }

        public string ConnectionString => InternalGetConnectionString();

        public IDbDateTimeProvider DbDateTimeProvider { get; set; }

        public ISqlSession DefaultSession
        {
            get => _defaultSession ?? (_defaultSession = InternalCreateSqlSession());
            internal set => _defaultSession = value;
        }

        public string Name { get; }

        public EDatabaseOption Options { get; set; }

        public bool CheckConnection(out string error)
        {
            try
            {
                InternalGetDateTime(connectionTimeout: 5);
                error = null;

                return true;
            }
            catch(DbException ex)
            {
                error = XHelper.Exceptions.GetMessageOnTopOfStack(ex);

                return false;
            }
        }

        public SqlAccess GetAccess() => DefaultSession.GetAccess().CastTo<SqlAccess>();

        public DateTime GetDateTime() => InternalGetDateTime();

        public DbConnection NewConnection() => InternalCreateConnection(ConnectionString);

        public ISqlSession NewSession() => InternalCreateSqlSession();

        protected abstract DbConnection InternalCreateConnection(string connectionString);

        protected abstract ISqlSession InternalCreateSqlSession();

        protected abstract string InternalGetConnectionString();

        protected abstract DateTime InternalGetDateTime(int connectionTimeout = 5);
    }
}