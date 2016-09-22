using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    class LinkedList<T> : IEnumerable<T>, ILinkedList<T>, IAdd<T>
    {
        Node<T> first, last;

        public LinkedList()
        {
            first = last = default(T);
        }

        public void Remove(int index)
        {
            var prev = GetNode(index - 1);
            prev += prev.Next.Next;
            Count--;
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            last += item;
            Count++;
        }

        public T Get(int index)
        {
            return GetNode(index);
        }

        private Node<T> GetNode(int index)
        {
            var node = first;
            while (index-- >= 0)
            {
                node++;
            }

            return node;
        }

        public T this[int index]
        {
            get
            {
                return Get(index);
            }
            set
            {
                GetNode(index).Item = value;
            }
        }

        public bool Contains(IComparable<T> item) => Contains(x => item.CompareTo(x) == 0);

        public IEnumerator<T> GetEnumerator()
        {
            var current = first;
            while (++current)
            {
                yield return current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void AddRange(ILinkedList<T> list)
        {
            foreach (var item in list)
            {
                Add(item);
            }
        }

        public bool Contains(T item, IEqualityComparer<T> comparer) => Contains(x => comparer.Equals(x, item));

        public bool Contains(Func<T, bool> method)
        {
            foreach (var item in Matching(method))
            {
                return true;
            }

            return false;
        }

        public IEnumerable<T> Matching(Func<T, bool> pred)
        {
            foreach (var item in this)
            {
                if (pred(item))
                {
                    yield return item;
                }
            }
        }

        public static LinkedList<T> operator +(LinkedList<T> list, T item)
        {
            var result = list.ToLinkedList();
            result.Add(item);

            return result;
        }

        public static LinkedList<T> operator +(LinkedList<T> lista, LinkedList<T> listb)
        {
            var result = lista.ToLinkedList();
            result.AddRange(listb);

            return result;
        }
    }
}