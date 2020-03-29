using System;
using System.Data;
using System.Data.Common;

// ReSharper disable CheckNamespace
namespace DotNetAppBase.Std.Db.SqlServer {
    public static class XDbExtensions
// ReSharper restore CheckNamespace
    {
        //public static DbParameter Set(this DbParameter param, string name, DbType dbType, object value)
        //{
        //    param.ParameterName = name;
        //    param.DbType = dbType;
        //    param.Value = Translate(value);

        //    return param;
        //}

        private static object Translate(object value) => value ?? DBNull.Value;
    }
}