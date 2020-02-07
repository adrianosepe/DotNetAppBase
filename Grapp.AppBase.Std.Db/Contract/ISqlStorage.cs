using System;
using System.Linq;

namespace Grapp.ApplicationBase.Db.Contract
{
	public interface ISqlStorage
	{
		ISqlDatabase DefaultDatabase { get; set; }

		bool Constains(string name);

		ISqlDatabase Restore(string name);

		bool Storage(ISqlDatabase database);

		bool UnStorage(SqlDatabase dataBase);
	}
}