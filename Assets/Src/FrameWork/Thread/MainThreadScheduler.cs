using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HG
{
    public class MainThreadScheduler:SingletonMono<MainThreadScheduler>
    {
        private readonly Queue<Action> _immediateQueue = new Queue<Action>();
        
        private int _mainThreadId;
        private void Awake()
        {
            _mainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            StartCoroutine(CheckTask());
        }

        public void Init()
        {
            
        }

        private bool IsMainThread()
        {
            return System.Threading.Thread.CurrentThread.ManagedThreadId == _mainThreadId;
        }

        private IEnumerator CheckTask()
        {
            while (true)
            {
                yield return null;

                while (_immediateQueue.Count > 0)
                {
                    _immediateQueue.Dequeue()();
                }
            }
        }

        public void Immediate(Action action)
        {
            if (IsMainThread())
            {
                action();
                return;
            }
            
            _immediateQueue.Enqueue(action);
        }
        
        public void Delay(float delay, Action action)
        {
            if (IsMainThread())
            {
                if (Math.Abs(delay) < Time.deltaTime)
                {
                    action();
                }
                else
                {
                    StartCoroutine(WaitForTime(delay, action));
                }
            }
            else
            {
                _immediateQueue.Enqueue(() => { StartCoroutine(WaitForTime(delay, action));});    
            }
        }

        private IEnumerator WaitForTime(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action();
        }
    }
}