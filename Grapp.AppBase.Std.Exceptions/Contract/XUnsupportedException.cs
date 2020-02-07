using System;
using System.ComponentModel;
using System.Linq;
using Grapp.AppBase.Std.Exceptions.Base;

namespace Grapp.AppBase.Std.Exceptions.Contract
{
	public class XUnsupportedException : XException
	{
		protected XUnsupportedException([Localizable(isLocalizable: false)] string message) : base(message) { }

		public static XUnsupportedException Create(string message) => new XUnsupportedException(message);

	    public static XUnsupportedException TypeDoesNotSupport(Type type, string operationDetail) 
	        => new XUnsupportedException(message: $"The type {type.Name} does not support operation: {operationDetail}");
	}
}