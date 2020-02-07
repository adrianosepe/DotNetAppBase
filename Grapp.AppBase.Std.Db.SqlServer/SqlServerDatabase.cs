using System;
using System.ComponentModel;
using System.Data.Common;
using System.Security;
using Grapp.ApplicationBase.Db;
using Grapp.ApplicationBase.Db.Contract;
using Microsoft.Data.SqlClient;

namespace Grapp.AppBase.Std.Db.SqlServer.Db
{
    [Localizable(isLocalizable: false)]
    public class SqlServerDatabase : SqlDatabase
    {
        private readonly SqlCredential _credential;
        private readonly Lazy<SqlConnectionStringBuilder> _lazyConnStrBuilder;

        private string _connectionString;

        public SqlServerDatabase(string name, string connectionString, string user, SecureString pwd) : base(name)
        {
            _connectionString = connectionString;
            _credential = new SqlCredential(user, pwd);

            _lazyConnStrBuilder = new Lazy<SqlConnectionStringBuilder>(
                () => new SqlConnectionStringBuilder(_connectionString));
        }

        public SqlServerDatabase(string name, string connectionString) : base(name)
        {
            _connectionString = connectionString;

            _lazyConnStrBuilder = new Lazy<SqlConnectionStringBuilder>(
                () => new SqlConnectionStringBuilder(_connectionString));
        }

        public override int ConnectionTimeout
        {
            get => Builder.ConnectTimeout;
            set
            {
                if(ConnectionTimeout != value)
                {
                    Builder.ConnectTimeout = value;

                    _connectionString = Builder.ToString();
                }
            }
        }

        private SqlConnectionStringBuilder Builder => _lazyConnStrBuilder.Value;

        protected override DbConnection InternalCreateConnection(string connectionString) => new SqlConnection(connectionString, _credential);

        protected override ISqlSession InternalCreateSqlSession() => new SqlServerSession(sqlServerDatabase: this);

        protected override string InternalGetConnectionString() => _connectionString;

        protected override DateTime InternalGetDateTime(int connectionTimeout = 5)
        {
            var local = new SqlConnectionStringBuilder(_connectionString)
                {
                    ConnectTimeout = connectionTimeout
                };

            using(var conn = InternalCreateConnection(connectionString: local.ToString()))
            using(var comm = conn.CreateCommand())
            {
                conn.Open();

                comm.CommandText = "SELECT GETDATE()";
                var dt = (DateTime)comm.ExecuteScalar();

                return dt;
            }
        }
    }
}