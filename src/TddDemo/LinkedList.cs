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
        Node first, last;

        public LinkedList()
        {
            first = last = new Node(default(T));
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            last = last.Next = new Node(item);
            Count++;
        }

        public T Get(int index)
        {
            var node = first;

            // dit is een grapje van Eric Lippert!
            while (index-- >= 0)
            {
                node = node.Next;
            }

            return node.Item;
        }

        public T this[int index] => Get(index);

        public bool Contains(IComparable<T> item)
        {
            foreach (var here in this)
            {
                if (item.CompareTo(here) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = first.Next;
            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        class Node
        {
            public Node(T item)
            {
                this.Item = item;
            }

            public T Item { get; }
            public Node Next { get; set; }
        }

        public void AddRange(ILinkedList<T> list)
        {
            foreach (var item in list)
            {
                Add(item);
            }
        }

        public bool Contains(T item, IEqualityComparer<T> comparer)
        {
            foreach (var element in this)
            {
                if (comparer.Equals(element, item))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Contains(Func<T, bool> method)
        {
            foreach (var item in this)
            {
                if (method(item))
                {
                    return true;
                }
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