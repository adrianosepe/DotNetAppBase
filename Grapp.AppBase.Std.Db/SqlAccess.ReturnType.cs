using System;

namespace Grapp.ApplicationBase.Db
{
	public partial class SqlAccess
	{
		public enum EReturnType
		{
			Unknown,

			DataSet,

			DataTable,

			DataRow,

			DataReader,

			Count,

			ProcReturn,

			ProcReturnAndValue
		}
	}
}