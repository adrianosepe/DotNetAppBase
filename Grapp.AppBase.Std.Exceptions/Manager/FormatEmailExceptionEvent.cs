using System;
using System.Linq;
using System.Text;

namespace Grapp.AppBase.Std.Exceptions.Manager
{
	public class FormatEmailExceptionEventArgs : EventArgs
	{
		public FormatEmailExceptionEventArgs(Exception exception)
		{
			Exception = exception;

			Output = new StringBuilder();
		}

		public Exception Exception { get; }

		public StringBuilder Output { get; }
	}
}