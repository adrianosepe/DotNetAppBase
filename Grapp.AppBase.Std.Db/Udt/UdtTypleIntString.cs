using System.Data;
using Grapp.AppBase.Std.Library;

namespace Grapp.ApplicationBase.Db.Udt
{
	public class UdtTypleIntString
	{
		private readonly DataTable _data;

		public UdtTypleIntString()
		{
			_data = new DataTable
				{
					Columns =
						{
							new DataColumn(columnName: "Item1", dataType: typeof(int)),
							new DataColumn(columnName: "Item2", dataType: typeof(string))
						}
				};
		}

		public DataTable DataTable => _data;

		public void Add(int item1, string item2)
		{
			_data.Rows.Add(item1, XHelper.Sql.ToDbValue(item2));
		}

		public void Add(int? item1, string item2)
		{
			_data.Rows.Add(XHelper.Sql.ToDbValue(item1), XHelper.Sql.ToDbValue(item2));
		}
	}
}