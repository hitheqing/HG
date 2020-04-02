﻿/********************************************************************
	Created:	2018-12-03
	Filename: 	LazyObserver.cs
	Author:		Heq
	QQ:         372058864
*********************************************************************/

using System;
using System.Collections.Generic;

namespace HG
{
    /*
     * 1.只用于监听消息
     * 2.一个事件id 只 对应一个监听，一个对象上重复监听无意义
     * 3.调用clear接口 针对当前的监听 进行移除，不需要一个个移除
     */
    public sealed class LazyObserver
    {
        private readonly Dictionary<int, IDisposable> m_record = new Dictionary<int, IDisposable>();

        public PriorityEventObserver Subscribe(int eventId, Action<object[]> func, int priority = 100)
        {
            if (m_record.ContainsKey(eventId))
            {
                return null;
            }

            PriorityEventObserver observer = new PriorityEventObserver(func, priority);
            
            m_record.Add(eventId, observer);
            EventMgr.Instance.AddObserver(eventId, observer);
            
            return observer;
        }

        public void Clear()
        {
            foreach (var pair in m_record)
            {
                pair.Value.Dispose();
            }
        }

        public PriorityEventObserver<T> Subscribe<T>(int eventId, Action<T> func, int priority = 100)
        {
            if (m_record.ContainsKey(eventId))
            {
                return null;
            }

            PriorityEventObserver<T> observer = new PriorityEventObserver<T>(func, priority);
            
            m_record.Add(eventId, observer);
            EventMgr.Instance.AddObserver(eventId, observer);
            
            return observer;
        }
    }
}