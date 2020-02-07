using System;
using System.Linq;
using Grapp.AppBase.Std.Exceptions.Base;

namespace Grapp.AppBase.Std.Exceptions.Bussines
{
	public abstract class XBussinesException : XException
	{
		protected XBussinesException() { }

		protected XBussinesException(string message) : base(message) { }

		protected XBussinesException(string message, Exception innerException) : base(message, innerException) { }
	}
}