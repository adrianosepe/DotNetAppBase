using System;
using System.Data.Common;
using System.Linq;

namespace Grapp.ApplicationBase.Db.Contract
{
    public interface ISqlTransactionManager
    {
        DbConnection Connection { get; }

        bool InTransaction { get; }

        ISqlSession Session { get; }

        DbTransaction Transaction { get; }

        void Commit();

        void Rollback();

        void StartTransaction();
    }
}