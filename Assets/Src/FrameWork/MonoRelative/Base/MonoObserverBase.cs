using System;

namespace HG
{
    public class MonoObserverBase:MonoBase
    {
        private LazyObserver Lo = new LazyObserver();

        protected void Subscribe(int eventId, Action<object[]> func, int priority = 100)
        {
            Lo.Subscribe(eventId, func, priority);
        }
        
        protected override void OnDestroy()
        {
            Lo.Clear();
            Lo = null;
            base.OnDestroy();
        }
    }
}