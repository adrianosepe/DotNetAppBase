using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Grapp.AppBase.Std.Library.ComponentModel.Types
{
	internal class XTypeDescriptorCache
	{
		private readonly ConcurrentDictionary<Type, XTypeDescriptor> _items;

		public XTypeDescriptorCache()
		{
			_items = new ConcurrentDictionary<Type, XTypeDescriptor>();
		}

		public XTypeDescriptor Get(Type type, Func<Type, XTypeDescriptor> factory)
		{
			return _items.GetOrAdd(type, factory);
		}
	}
}