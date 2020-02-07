using System;
using System.ComponentModel;
using System.Linq;

namespace Grapp.AppBase.Std.Exceptions.Bussines
{
	public class XFlowException : XBussinesException
	{
		private const string DefaultMessage = "The flow fail with this reason: {0}.";

		protected XFlowException(string failReason) : base(message: String.Format(DefaultMessage, failReason)) { }

		public static XFlowException Create([Localizable(isLocalizable: false)] string failReason) => new XFlowException(failReason);
	}
}