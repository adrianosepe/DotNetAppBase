using System.Data;

namespace Grapp.AppBase.Std.Library.Data.Udt 
{
    public abstract class UdtBase
    {
        public DataTable Table { get; }

        protected UdtBase(string tableName)
        {
            Table = new DataTable(tableName);
        }

        public DataColumnCollection Columns => Table.Columns;

        public DataRowCollection Rows => Table.Rows;
    }
}