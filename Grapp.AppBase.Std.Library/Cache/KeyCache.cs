using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Grapp.AppBase.Std.Exceptions.Assert;

namespace Grapp.AppBase.Std.Library.Cache
{
	public class KeyCache<TKey, TValue>
	{
		private readonly ConcurrentDictionary<TKey, TValue> _data;
		private readonly Func<TKey, TValue> _funcCreate;

		public KeyCache(Func<TKey, TValue> funcCreate)
		{
			XContract.ArgIsNotNull(funcCreate, nameof(funcCreate));

			_funcCreate = funcCreate;

			_data = new ConcurrentDictionary<TKey, TValue>();
		}

		public IEnumerable<TValue> Values => _data.Values;

		public TValue Get(TKey key)
		{
			return _data.GetOrAdd(key, _funcCreate);
		}
	}
}