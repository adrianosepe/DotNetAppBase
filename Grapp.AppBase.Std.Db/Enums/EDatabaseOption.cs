using System;

namespace Grapp.ApplicationBase.Db
{
	[Flags]
	public enum EDatabaseOption
	{
		/// <summary>
		///     Tentar novamente quando ocorrer um Timeout: SqlCode = 1222
		/// </summary>
		RetryDeadlock = 1,

		/// <summary>
		///     Tentar novamente quando ocorrer um Deadlock: SqlCode = 1205
		/// </summary>
		RetryTimeout = 2
	}
}