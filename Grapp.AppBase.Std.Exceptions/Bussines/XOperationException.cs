using System;
using System.Linq;

namespace Grapp.AppBase.Std.Exceptions.Bussines
{
	public class XOperationException : XBussinesException
	{
		private const string DefaultMessage = "The operation is not valid: {0}.";

		protected XOperationException(string failReason) : base(message: String.Format(DefaultMessage, failReason)) { }

		public static XOperationException Create(string failReason) => new XOperationException(failReason);
	}
}