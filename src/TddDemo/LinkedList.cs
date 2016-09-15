using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    class LinkedList<T> : IEnumerable<T>
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
    }
}