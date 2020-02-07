using System;

namespace Grapp.AppBase.Std.Library.Events
{
	public class FuncEventArgs<TParam, TResult> : EventArgs
	{
		public FuncEventArgs(TParam param)
		{
			Param = param;
		}

		public TParam Param { get; }

		public TResult Result { get; set; }

		public bool HasResult()
		{
			return !Equals(Result, default(TResult));
		}
	}
}