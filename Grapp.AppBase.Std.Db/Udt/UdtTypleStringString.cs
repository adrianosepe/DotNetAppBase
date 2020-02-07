using System.Data;
using Grapp.AppBase.Std.Library;

namespace Grapp.ApplicationBase.Db.Udt
{
	public class UdtTypleStringString
	{
		private readonly DataTable _data;

		public UdtTypleStringString()
		{
			_data = new DataTable
				{
					Columns =
						{
							new DataColumn(columnName: "Item1", dataType: typeof(string)),
							new DataColumn(columnName: "Item2", dataType: typeof(string))
						}
				};
		}

		public DataTable DataTable => _data;

		public void Add(string item1, string item2)
		{
			_data.Rows.Add(XHelper.Sql.ToDbValue(item2), XHelper.Sql.ToDbValue(item2));
		}
	}
}