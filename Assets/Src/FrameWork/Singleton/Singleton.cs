﻿namespace HG
{
    public abstract class Singleton<T> where T:class ,new()
    {
        private static T _inst;
        
        private static readonly object ResLock = new object();
        
        public static T Instance
        {
            get
            {
                if (_inst == null)
                {
                    lock (ResLock)
                    {
                        if (_inst == null)
                        {
                            _inst = new T();
                        }
                    }
                }

                return _inst;
            }
        }
    }
}