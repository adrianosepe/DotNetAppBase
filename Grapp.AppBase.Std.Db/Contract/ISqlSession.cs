using System;
using System.Data.Common;
using System.Linq;

namespace Grapp.ApplicationBase.Db.Contract
{
    public interface ISqlSession : ISqlAccessProvider
    {
        ISqlDatabase Database { get; }

        bool InTransaction { get; }

        ISqlTransactionManager TransactionManager { get; }

        void BeginTransaction();

        ISqlContext BuildContext();

        void Commit();

        DbParameter CreateReturnParameter();

        void Rollback();

        DbDataAdapter CreateDataAtapter(DbCommand cmd);

        bool RetryInteractionOnDbExcepion(DbException exception);
    }
}