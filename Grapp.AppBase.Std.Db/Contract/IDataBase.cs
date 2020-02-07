using System;
using System.Linq;

namespace Grapp.ApplicationBase.Db.Contract
{
	public interface IDatabase : IDbDateTimeProvider
	{
		bool CheckConnection(out string error);
	}
}