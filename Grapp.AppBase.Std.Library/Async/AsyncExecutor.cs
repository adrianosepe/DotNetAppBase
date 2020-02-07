using System;

namespace Grapp.AppBase.Std.Library.Async
{
	public class AsyncExecutor<TData>
	{
		private readonly Func<TData> _execute;

		private readonly object _sync = new object();

		private System.Threading.Tasks.Task _task;
		private TData _value;

		public AsyncExecutor(Func<TData> execute, bool autoBeginInvoke = true)
		{
			_execute = execute;

			if(autoBeginInvoke)
			{
				BeginInvoke();
			}
		}

		public TData ReadValue(int milisecondsTimeout = Int32.MaxValue)
		{
			lock (_sync)
			{
				_task?.Wait(milisecondsTimeout);

				return _value;
			}
		}

		public bool BeginInvoke()
		{
			lock(_sync)
			{
				if(_task != null)
				{
					return false;
				}

				_task = System.Threading.Tasks.Task.Run(
					() =>
						{
							_value = _execute();

							_task = null;
						});

				return true;
			}
		}
	}
}