﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HG
{
    /// <summary>
    /// 优先级队列，二叉树，小堆
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public  class PriorityQueue<T> where T : IComparable<T>
    {
        private static long _count = long.MinValue;

        private IndexedItem[] _items;
        private int _size;
        private readonly bool _heapMin;

        public PriorityQueue(bool heapMin = true)
            : this(16, heapMin)
        {
        }

        public PriorityQueue(int capacity,bool heapMin = true)
        {
            _items = new IndexedItem[capacity];
            _size = 0;
            _heapMin = heapMin;
        }

        private bool IsHigherPriority(int left, int right)
        {
            if (_heapMin)
            {
                  return _items[left].CompareTo(_items[right]) < 0;
            }
            
            return _items[left].CompareTo(_items[right]) > 0;
        }

        private void Percolate(int index)
        {
            if (index >= _size || index < 0)
                return;
            var parent = (index - 1) / 2;
            if (parent < 0 || parent == index)
                return;

            if (IsHigherPriority(index, parent))
            {
                SwapSkipCheck(index, parent);
                Percolate(parent);
            }
        }

        private void Heapify()
        {
            Heapify(0);
        }

        private void Heapify(int index)
        {
            if (index >= _size || index < 0)
                return;

            var left = 2 * index + 1;
            var right = 2 * index + 2;
            var first = index;

            if (left < _size && IsHigherPriority(left, first))
                first = left;
            if (right < _size && IsHigherPriority(right, first))
                first = right;
            if (first != index)
            {
                SwapSkipCheck(index, first);
                Heapify(first);
            }
        }

        public int Count { get { return _size; } }

        public T Peek()
        {
            if (_size == 0)
                return default(T);
//                throw new InvalidOperationException("HEAP is Empty");

            return _items[0].Value;
        }

        private void RemoveAt(int index)
        {
            _items[index] = _items[--_size];
            _items[_size] = default(IndexedItem);
            Heapify();
            if (_size < _items.Length / 4)
            {
                var temp = _items;
                _items = new IndexedItem[_items.Length / 2];
                Array.Copy(temp, 0, _items, 0, _size);
            }
        }

        public void Clear()
        {
            Array.Clear(_items, 0, _size);
            _size = 0;
        }

        public T Dequeue()
        {
            var result = Peek();
            RemoveAt(0);
            return result;
        }

        public void Enqueue(T item)
        {
            if (_size >= _items.Length)
            {
                var temp = _items;
                _items = new IndexedItem[_items.Length * 2];
                Array.Copy(temp, _items, temp.Length);
            }

            var index = _size++;
            _items[index] = new IndexedItem { Value = item, Id = Interlocked.Increment(ref _count) };
            Percolate(index);
        }

        public bool Remove(T item)
        {
            for (var i = 0; i < _size; ++i)
            {
                if (EqualityComparer<T>.Default.Equals(_items[i].Value, item))
                {
                    RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        private void SwapSkipCheck(int left, int right)
        {
            var temp = _items[left];
            _items[left] = _items[right];
            _items[right] = temp;
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

        public T[] CopySortedArray()
        {
            IndexedItem[] itemsNew = new IndexedItem[_size];
            Array.Copy(_items, itemsNew, _size);
            
            for (int i = 0; i < _size - 1; i++)
            {
                for (int j = 0; j < _size - 1 - i; j++)
                {
                    if (IsHigherPriority(j, j + 1))
                    {
                        var temp = itemsNew[j];
                        itemsNew[j] = itemsNew[j+1];
                        itemsNew[j+1] = temp;
                    }
                }
            }

            return itemsNew.Select(x => x.Value).ToArray();
        }

        private struct IndexedItem : IComparable<IndexedItem>
        {
            public T Value;
            public long Id;

            public int CompareTo(IndexedItem other)
            {
                var c = Value.CompareTo(other.Value);
                return c == 0 ? Id.CompareTo(other.Id) : c;
            }
        }
    }
}