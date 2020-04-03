using System;
using System.Collections;
using System.Collections.Generic;

namespace HG
{
    public class BiList<T> : IReadOnlyList<T>
    {
        private const int Mincapacity = 0xF;

        private int _head;
        private int _tail;
        private int _size;
        private T[] _items;
        private int _capacity;

        public BiList() : this(Mincapacity)
        {
        }

        public BiList(int capacity)
        {
            _capacity = capacity < Mincapacity ? Mincapacity : capacity;
            _items    = new T[_capacity];
            _head     = 0;
            _tail     = 0;
            _size     = 0;
        }

        public void AddFirst(T t)
        {
            ValidFirstElement(t);

            if (_head == 0)
            {
                Prepare();
            }

            _items[--_head] = t;
            _size++;
        }

        public void AddLast(T t)
        {
            ValidFirstElement(t);

            if (_tail == _items.Length - 1)
            {
                Prepare();
            }

            _items[++_tail] = t;
            _size++;
        }

        public void Clear()
        {
            Array.Clear(_items, 0, _items.Length);
            _head = 0;
            _tail = 0;
            _size = 0;
        }

        public void RemoveAt(int index)
        {
            IndexCheck(index);

            var realIndex = _head + index;

            //经测试，移除的时候采用method2  效率更高
            //method 1
            //            {
            //                _size--;
            //                Array.Copy(_items, realIndex + 1, _items, realIndex, _tail - realIndex + 1);
            //            }

            //method 2
            if (index <= (_size - 1) / 2)
            {
                for (int i = realIndex; i > _head; i--)
                {
                    _items[i] = _items[i - 1];
                }

                _items[_head++] = default(T);
                _size--;
            }
            else
            {
                for (int i = realIndex; i < _tail; i++)
                {
                    _items[i] = _items[i + 1];
                }

                _items[_tail--] = default(T);
                _size--;
            }

            if (_size < _items.Length / 4)
            {
                Prepare();
            }
        }

        public bool Remove(T t)
        {
            for (int i = _head; i < _size; i++)
            {
                if (EqualityComparer<T>.Default.Equals(_items[i], t))
                {
                    RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        private void Prepare()
        {
            _capacity = _size * 3;
            var temp = _items;
            _items = new T[_capacity];
            Array.Copy(temp, _head, _items, _size, _size);
            _head = _size;
            _tail = 2 * _size - 1;
        }

        private void IndexCheck(int index)
        {
            if (index < 0 || index > _size - 1)
            {
                throw new IndexOutOfRangeException();
            }
        }

        private void ValidFirstElement(T t)
        {
            if (_size == 0)
            {
                _head         = _tail = (_capacity - 1) / 2;
                _items[_head] = t;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var index = _head - 1;
            while (++index <= _tail)
            {
                yield return _items[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return _size; }
        }

        public T this[int index]
        {
            get
            {
                IndexCheck(index);

                return _items[_head + index];
            }
        }
    }
}