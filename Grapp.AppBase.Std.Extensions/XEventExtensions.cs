using System;

namespace Grapp.ApplicationBase.Extensions
{
	public static class XEventExtensions
	{
		public static void Raise(this EventHandler eventHandler, object sender)
		{
			eventHandler?.Invoke(sender, EventArgs.Empty);
		}

		public static void Raise<TEventArgs>(this EventHandler<TEventArgs> eventHandler,
		                                     object sender, Func<TEventArgs> funcGetArgs)
			where TEventArgs : EventArgs
		{
			eventHandler?.Invoke(sender, e: funcGetArgs());
		}
	}
}