﻿namespace Grapp.AppBase.Std.Library.Events
{
	public delegate void StateChangedEventHandler<TState>(object sender, StateChangedEventArgs<TState> args);
}