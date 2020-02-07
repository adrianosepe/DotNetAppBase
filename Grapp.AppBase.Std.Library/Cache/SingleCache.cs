using System;
using Grapp.AppBase.Std.Library.Concurrency;

namespace Grapp.AppBase.Std.Library.Cache
{
    public class SingleCache<T> : XLazy<T>
    {
        private readonly TimeSpan _invalidate;

        private DateTime _lastInvalidate;

        public SingleCache(TimeSpan invalidate, Func<T> valueFactory, Action<T> releaseAction = null, Action<T> onCreated = null) 
            : base(valueFactory, releaseAction, onCreated)
        {
            _invalidate = invalidate;
        }

        protected override void CheckToInvalidate()
        {
            base.CheckToInvalidate();

            var now = DateTime.Now;
            if (now + _invalidate > _lastInvalidate)
            {
                Reset();

                _lastInvalidate = now;
            }
        }
    }
}