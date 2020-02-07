using System;
using System.Linq;
using Grapp.AppBase.Std.Library.ComponentModel.Collection.Events;

namespace Grapp.AppBase.Std.Library.ComponentModel.Collection
{
	public interface INotifyItemAdded<TItem> where TItem : class
	{
		event EventHandler<EntityActionEventArgs<TItem>> ItemsChanged;
	}
}