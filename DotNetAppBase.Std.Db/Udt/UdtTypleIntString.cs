using System.Data;
using DotNetAppBase.Std.Library;

namespace DotNetAppBase.Std.Db.Udt
{
	public class UdtTypleIntString
	{
        public UdtTypleIntString() =>
            DataTable = new DataTable
                {
                    Columns =
                        {
                            new DataColumn("Item1", typeof(int)),
                            new DataColumn("Item2", typeof(string))
                        }
                };

        public DataTable DataTable { get; }

        public void Add(int item1, string item2)
		{
			DataTable.Rows.Add(item1, XHelper.Sql.ToDbValue(item2));
		}

		public void Add(int? item1, string item2)
		{
			DataTable.Rows.Add(XHelper.Sql.ToDbValue(item1), XHelper.Sql.ToDbValue(item2));
		}
	}
}