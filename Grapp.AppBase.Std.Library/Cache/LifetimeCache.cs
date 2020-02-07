using System;

namespace Grapp.AppBase.Std.Library.Cache
{
	public class LifetimeCache<TValue>
	{
		private readonly Func<TValue> _funcCreate;
		private readonly object _syncReadValue = new object();

		private TValue _value;
		private DateTime _cached;

		public LifetimeCache(long lifetime, Func<TValue> funcCreate)
		{
			_funcCreate = funcCreate;

			Lifetime = lifetime;
		}

		public long Lifetime { get; }

		public TValue Value
		{
			get
			{
				lock(_syncReadValue)
				{
					var now = DateTime.Now;

					if(_cached.AddMilliseconds(Lifetime) < now)
					{
						_value = _funcCreate();
						_cached = now;
					}

					return _value;
				}
			}
		}
	}
}