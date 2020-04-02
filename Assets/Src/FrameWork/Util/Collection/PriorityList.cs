﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace HG
{
    /// <summary>
    /// 优先级列表，添加元素后自动排序，
    /// 
    /// </summary>
    public class PriorityList<T> : IReadOnlyList<T> where T : IComparable<T>
    {
        
        private int _size;
        private static int _count;
        public int Count
        {
            get { return _size; }
        }
        private IndexedItem[] _items;
        private int _capacity;
        
        public PriorityList():this(4)
        {
        }

        public PriorityList(int capacity)
        {
            _capacity = capacity;
            _items = new IndexedItem[capacity];
            _size = 0;
        }

        public PriorityList(IReadOnlyList<T> array)
        {
            _size = 0;
            _capacity = array.Count;
            _items = new IndexedItem[_capacity];

            for (int i = 0; i < array.Count; i++)
            {
                Add(array[i]);
            }
        }
        
        private bool IsHigherPriority(int left, int right)
        {
            return _items[left].CompareTo(_items[right]) < 0;
        }

        public void Clear()
        {
            Array.Clear(_items, 0, _size);
            _size = 0;
        }

        public void Add(T t)
        {
            if (_size >= _items.Length)
            {
                var temp = _items;
                _items = new IndexedItem[_items.Length * 2];
                Array.Copy(temp, _items, temp.Length);
            }

            var index = _size++;
            _items[index] = new IndexedItem() {Value = t, Id = Interlocked.Increment(ref _count)};
            Prewave(index);
        }

        private void Prewave(int index)
        {
            if (index <= 0 || index > _size - 1) 
            {
                return;
            }

            var pre = index - 1;

            if (IsHigherPriority(index, pre))
            {
                SwapSkipCheck(index, pre);
                Prewave(pre);
            }
        }

        private void SwapSkipCheck(int left, int right)
        {
            var temp = _items[left];
            _items[left] = _items[right];
            _items[right] = temp;
        }
        
        public bool Remove(T t)
        {
            for (int i = 0; i < _size; i++)
            {
                if (EqualityComparer<T>.Default.Equals(_items[i].Value, t))
                {
                    RemoveAt(i);
                    return true;
                }
            }

            return false;
        }
        
        public void RemoveAt(int index)
        {
            if (index < 0 || index > _size - 1)
            {
                throw new IndexOutOfRangeException();
            }

            if (index == _size - 1)
            {
                _items[index] = default(IndexedItem);
                _size--;
            }
            else
            {
                for (int i = 0; i < _size; i++)
                {
                    if (i >= index)
                    {
                        _items[i] = _items[i + 1];
                    }
                }
                _items[--_size ] = default(IndexedItem);
            }
            
            if (_size < _items.Length / 4)
            {
                var temp = _items;
                _items = new IndexedItem[_items.Length / 2];
                Array.Copy(temp, 0, _items, 0, _size);
            }
        }
        
        
        struct IndexedItem : IComparable<IndexedItem>
        {
            public T Value;
            public long Id;

            public int CompareTo(IndexedItem other)
            {
                var c = Value.CompareTo(other.Value);
                return c == 0 ? Id.CompareTo(other.Id) : c;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            var index = -1;
            while (++index < _size)
            {
                yield return _items[index].Value;
            }
        }

        /// 使用yield 语法糖 ，比new一个实现IEnumerator的对象效率更高
        public IEnumerator GetEnumerator()
        {
            var index = -1;
            while (++index < _size)
            {
                yield return _items[index].Value;
            }
        }
        
        public bool RemoveBy(Func<T, bool> func)
        {
            for (int i = 0; i < _size; i++)
            {
                if (func(_items[i].Value))
                {
                    RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index > _size - 1) throw new IndexOutOfRangeException();
                return _items[index].Value;
            }
        }
    }
}