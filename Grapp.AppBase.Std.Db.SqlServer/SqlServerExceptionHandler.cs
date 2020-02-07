using Grapp.ApplicationBase.Db;
using Grapp.ApplicationBase.Db.Contract;
using Microsoft.Data.SqlClient;

namespace Grapp.AppBase.Std.Db.SqlServer.Db
{
    public static class SqlServerExceptionHandler
    {
        public const int TimeoutErrorCode = 1222;
        public const int DeadlockErrorCode = 1205;

        public const int TransportLevelErrorCode = 121;
        public const int TransportLevelSeverityClass = 20;

        public static bool RetryInteraction(ISqlDatabase database, SqlException exception)
        {
            if(exception.Number == TimeoutErrorCode || exception.Number == -2)
            {
                return (database.Options & EDatabaseOption.RetryTimeout) == EDatabaseOption.RetryTimeout;
            }

            if(exception.Number == DeadlockErrorCode)
            {
                return (database.Options & EDatabaseOption.RetryDeadlock) == EDatabaseOption.RetryDeadlock;
            }

            if(exception.Number == DeadlockErrorCode)
            {
                return (database.Options & EDatabaseOption.RetryDeadlock) == EDatabaseOption.RetryDeadlock;
            }

            if(exception.Number == TransportLevelErrorCode && exception.Class == TransportLevelSeverityClass)
            {
                SqlConnection.ClearAllPools();

                return true;
            }

            return false;
        }
    }
}