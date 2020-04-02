﻿/********************************************************************
	Created:	2018-12-03
	Filename: 	PriorityEventObserver.cs
	Author:		Heq
	QQ:         372058864
*********************************************************************/

using System;

namespace HG
{
    /// <summary>
    /// 优先级的事件观察者
    /// 顾名思义，需要，实现优先级判断，优先级字段，订阅的事件名
    /// </summary>
    public class PriorityEventObserver : IObserver<object[]>, IComparable<PriorityEventObserver>, IDisposable
    {
        private readonly int m_priority;
        private readonly Action<object[]> m_onNext;
        private IDisposable m_disposable;

        public PriorityEventObserver(Action<object[]> onNext, int priority)
        {
            m_onNext = onNext;
            m_priority = priority;
        }

        public void OnNext(object[] value)
        {
            m_onNext(value);
        }

        public void OnError(Exception error)
        {
        }

        public void OnCompleted()
        {
        }

        public int CompareTo(PriorityEventObserver other)
        {
            return m_priority.CompareTo(other.m_priority);
        }

        public void SubScribe(IObservable<object[]> provider)
        {
            if (m_disposable != null)
            {
                Loger.Error("repeated subscribe");
                m_disposable.Dispose();
            }
            
            m_disposable = provider.Subscribe(this);    
        }
        
        public void Dispose()
        {
            m_disposable?.Dispose();
        }
    }
    
    public class PriorityEventObserver<T> : IObserver<T>, IComparable<PriorityEventObserver<T>>, IDisposable
    {
        private readonly int m_priority;
        private readonly Action<T> m_onNext;
        private IDisposable m_disposable;

        public PriorityEventObserver(Action<T> onNext, int priority)
        {
            m_onNext = onNext;
            m_priority = priority;
        }

        public void OnNext(T value)
        {
            m_onNext(value);
        }

        public void OnError(Exception error)
        {
        }

        public void OnCompleted()
        {
        }

        public int CompareTo(PriorityEventObserver<T> other)
        {
            return m_priority.CompareTo(other.m_priority);
        }

        public void SubScribe(IObservable<T> provider)
        {
            if (m_disposable != null)
            {
                Loger.Error("repeated subscribe");
                m_disposable.Dispose();
            }
            
            m_disposable = provider.Subscribe(this);    
        }
        
        public void Dispose()
        {
            m_disposable?.Dispose();
        }
    }
}