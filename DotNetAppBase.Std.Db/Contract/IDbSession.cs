using System.Data.Common;

namespace DotNetAppBase.Std.Db.Contract
{
    public interface IDbSession : IDbAccessProvider
    {
        IDbDatabase Database { get; }

        bool InTransaction { get; }

        IDbTransactionManager TransactionManager { get; }

        void BeginTransaction();

        IDbContext BuildContext();

        void Commit();

        DbParameter CreateReturnParameter();

        void Rollback();

        DbDataAdapter CreateDataAtapter(DbCommand cmd);

        bool RetryInteractionOnDbExcepion(DbException exception);
    }
}