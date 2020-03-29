using System.Data;
using System.Data.Common;
using DotNetAppBase.Std.Db.Contract;

#if NETFRAMEWORK
using System.Data.SqlClient;
#else
using Microsoft.Data.SqlClient;
#endif

namespace DotNetAppBase.Std.Db.SqlServer 
{
    public class DbServerSession : DbSession
    {
        public DbServerSession(IDbDatabase dbServerDatabase) : base(dbServerDatabase) { }

        public override DbParameter CreateReturnParameter() => new SqlParameter {ParameterName = "@RETURN_VALUE", Direction = ParameterDirection.ReturnValue};

        public override DbDataAdapter CreateDataAtapter(DbCommand cmd) => new SqlDataAdapter(cmd.CastTo<SqlCommand>());

        public override bool RetryInteractionOnDbExcepion(DbException exception) => SqlServerExceptionHandler.RetryInteraction(Database, exception.CastTo<SqlException>());
    }
}