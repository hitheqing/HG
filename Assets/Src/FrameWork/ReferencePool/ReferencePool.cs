﻿using System;
using System.Collections.Generic;

 namespace HG
{
    internal interface IRefCheck
    {
        void Check();
    }

    internal class ReferencePool<T> : IRefCheck where T : new ()
    {
        public ReferencePool(){}

        public ReferencePool(int initCap)
        {
            for (int i = 0; i < initCap; i++)
            {
                _buffer.Push(new T());
            }
        }

        private const int MaxCap = 50;

        private readonly Stack<T> _buffer = new Stack<T>();
        
        public T Get() => _buffer.Count > 0 ? _buffer.Pop() :new T();

        public void Push(T t) => _buffer.Push(t);

        public void Check()
        {
            var offset = _buffer.Count - MaxCap;
            while (offset > 0)
            {
                _buffer.Pop();
                offset--;
            }
        }
    }

    public class ReferenceMgr : Singleton<ReferenceMgr>
    {
        private readonly Dictionary<Type, IRefCheck> _pools = new Dictionary<Type, IRefCheck>();
        
        public void Check()
        {
            foreach (var t in _pools)
            {
                t.Value.Check();
            }
        }

        public T Get<T>() where T : new () => ((ReferencePool<T>) _pools[typeof(T)]).Get();

        public void Push<T>(T t) where T : new () => ((ReferencePool<T>) _pools[typeof(T)]).Push(t);

        public void RegType<T>(int cap = 10) where T : new() => _reg<T>(cap);

        private void _reg<T>(int cap) where T : new()
        {
            var pool = new ReferencePool<T>(cap);
            _pools[typeof(T)] = pool;
        }
    }
}