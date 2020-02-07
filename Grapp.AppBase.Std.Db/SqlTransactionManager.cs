using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using Grapp.ApplicationBase.Db.Contract;

namespace Grapp.ApplicationBase.Db
{
    public class SqlTransactionManager : ISqlTransactionManager
    {
        public SqlTransactionManager(ISqlSession session) => Session = session;

        public DbConnection Connection { get; private set; }

        public bool InTransaction => Transaction != null;

        public ISqlSession Session { get; }

        public DbTransaction Transaction { get; private set; }

        public void Commit()
        {
            if(!InTransaction)
            {
                return;
            }

            Transaction.Commit();

            Clean();
        }

        public void Rollback()
        {
            if(!InTransaction)
            {
                return;
            }

            Transaction.Rollback();

            Clean();
        }

        public void StartTransaction()
        {
            if(InTransaction)
            {
                return;
            }

            Connection = Session.Database.NewConnection();

            if(Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            Transaction = Connection.BeginTransaction();
        }

        private void Clean()
        {
            Connection.Dispose();
            Transaction.Dispose();

            Connection = null;
            Transaction = null;
        }
    }
}