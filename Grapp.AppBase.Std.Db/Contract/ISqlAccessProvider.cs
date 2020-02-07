using System;
using System.Linq;

namespace Grapp.ApplicationBase.Db.Contract
{
	public interface ISqlAccessProvider
	{
		ISqlAccess GetAccess();
	}
}