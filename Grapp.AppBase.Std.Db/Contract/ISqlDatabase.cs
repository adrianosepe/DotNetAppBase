using System;
using System.Data.Common;
using System.Linq;

namespace Grapp.ApplicationBase.Db.Contract
{
    public interface ISqlDatabase : IDatabase
    {
        int CommandTimeout { get; set; }

        string ConnectionString { get; }

        int ConnectionTimeout { get; set; }

        ISqlSession DefaultSession { get; }

        string Name { get; }

        EDatabaseOption Options { get; }

        SqlAccess GetAccess();

        DbConnection NewConnection();

        ISqlSession NewSession();
    }
}