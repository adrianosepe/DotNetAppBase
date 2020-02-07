using System.Data;
using System.Data.Common;
using Grapp.ApplicationBase.Db;
using Microsoft.Data.SqlClient;

namespace Grapp.AppBase.Std.Db.SqlServer.Db 
{
    public class SqlServerSession : SqlSession
    {
        public SqlServerSession(SqlServerDatabase sqlServerDatabase) : base(sqlServerDatabase) { }

        public override DbParameter CreateReturnParameter() => new SqlParameter {ParameterName = "@RETURN_VALUE", Direction = ParameterDirection.ReturnValue};

        public override DbDataAdapter CreateDataAtapter(DbCommand cmd) => new SqlDataAdapter(selectCommand: cmd.CastTo<SqlCommand>());

        public override bool RetryInteractionOnDbExcepion(DbException exception) => SqlServerExceptionHandler.RetryInteraction(Database, exception: exception.CastTo<SqlException>());
    }
}