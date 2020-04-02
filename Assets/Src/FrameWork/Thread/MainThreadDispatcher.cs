using System;

namespace HG
{
    public static class MainThreadDispatcher
    {
        public static void Immediate(Action action)
        {
            MainThreadScheduler.Instance.Immediate(action);
        }
        
        public static void Delay(float delay, Action action)
        {
            MainThreadScheduler.Instance.Delay(delay, action);
        }
    }
}