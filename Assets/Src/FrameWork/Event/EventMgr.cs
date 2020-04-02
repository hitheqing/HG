﻿/********************************************************************
	Created:	2018-12-03
	Filename: 	EventMgr.cs
	Author:		Heq
	QQ:         372058864
*********************************************************************/

using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace HG
{
    public partial class EventMgr : Singleton<EventMgr>
    {
        private readonly ConcurrentDictionary<int, object> m_map = new ConcurrentDictionary<int, object>();
        
        public void Clear()
        {
            m_map.Clear();
        }

        private EventObservable GetObservable(int eventId)
        {
            object obj;
            if (!m_map.TryGetValue(eventId, out obj))
            {
                obj = new EventObservable();
                m_map[eventId] = obj;
            }

            return (EventObservable) obj;
        }

        public void Notify(int eventId, params object[] para)
        {
            GetObservable(eventId).Notify(para);
        }

        public void ClearByEventId(int eventId)
        {
            object obj;
            m_map.TryGetValue(eventId, out obj);

            EventObservable observable = obj as EventObservable;
            observable?.ShutDown();
        }

        public PriorityEventObserver Subscribe(int eventId, Action<object[]> onNext, int priority = 100)
        {
            PriorityEventObserver observer = new PriorityEventObserver(onNext, priority);
            AddObserver(eventId, observer);
            return observer;
        }

        internal void AddObserver(int eventId, PriorityEventObserver observer)
        {
            observer.SubScribe(GetObservable(eventId));
        }

        /// <summary>
        /// remove shoud call from observer instead of map
        /// </summary>
        internal void RemoveObserver(PriorityEventObserver observer)
        {
            Debug.LogWarning("remove shoud call from observer instead of map!");
            observer.Dispose();
        }
    }
    
    /// <summary>
    /// 泛型方法，避免装箱拆箱
    /// </summary>
    public partial class EventMgr
    {
        private EventObservable<T> GetObservable<T>(int eventId)
        {
            object obj;
            if (!m_map.TryGetValue(eventId, out obj))
            {
                obj = new EventObservable<T>();
                m_map[eventId] = obj;
            }

            return (EventObservable<T>) obj;
        }

        public void Notify<T>(int eventId, T para)
        {
            GetObservable<T>(eventId).Notify(para);
        }

        public void ClearByEventId<T>(int eventId)
        {
            object obj;
            m_map.TryGetValue(eventId, out obj);

            EventObservable<T> observable = obj as EventObservable<T>;
            observable?.ShutDown();
        }

        public PriorityEventObserver<T> Subscribe<T>(int eventId, Action<T> onNext, int priority = 100)
        {
            PriorityEventObserver<T> observer = new PriorityEventObserver<T>(onNext, priority);
            AddObserver(eventId, observer);
            return observer;
        }

        internal void AddObserver<T>(int eventId, PriorityEventObserver<T> observer)
        {
            observer.SubScribe(GetObservable<T>(eventId));
        }

        /// <summary>
        /// remove shoud call from observer instead of map
        /// </summary>
        internal void RemoveObserver<T>(PriorityEventObserver<T> handle)
        {
            Debug.LogWarning("remove shoud call from observer instead of map!");
            handle.Dispose();
        }
    }

}