using System.Data;
using DotNetAppBase.Std.Library;

namespace DotNetAppBase.Std.Db.Udt
{
	public class UdtTypleStringString
	{
        public UdtTypleStringString() =>
            DataTable = new DataTable
                {
                    Columns =
                        {
                            new DataColumn("Item1", typeof(string)),
                            new DataColumn("Item2", typeof(string))
                        }
                };

        public DataTable DataTable { get; }

        public void Add(string item1, string item2)
		{
			DataTable.Rows.Add(XHelper.Sql.ToDbValue(item2), XHelper.Sql.ToDbValue(item2));
		}
	}
}