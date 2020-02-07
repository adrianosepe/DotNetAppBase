using System;
using System.ComponentModel;
using System.Linq;
using Grapp.AppBase.Std.Exceptions.Base;

namespace Grapp.AppBase.Std.Exceptions.Assert
{
	public class XContractFailException : XException
	{
		protected XContractFailException(string message) : base(message) { }

		[Localizable(isLocalizable: false)]
		public static XContractFailException Create(string message)
		{
			return new XContractFailException(message);
		}
	}
}