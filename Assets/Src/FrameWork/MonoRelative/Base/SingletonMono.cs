﻿using UnityEngine;

namespace HGame
{
    /*
     * 全程不销毁的
     * 恰当的时机检查一下内存情况
     */
    public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _inst;
        private static readonly object syslock = new object();

        public static T Inst
        {
            get
            {
                if (_inst == null)
                {
                    lock (syslock)
                    {
                        if (_inst == null)
                        {
                            _inst = FindObjectOfType(typeof(T)) as T;

                            if (_inst == null)
                            {
                                GameObject go = new GameObject("[" + typeof(T).Name + "]");
                                DontDestroyOnLoad(go); 
                                _inst = go.AddComponent<T>();
                            }
                        }
                    }
                }

                return _inst;
            }
        }
    }
}