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
        private Node first;

        public int Count { get; private set; }

        public void Add(T item)
        {
            if (first != null)
            {
                var node = first;
                while (node.Next != null)
                {
                    node = node.Next;
                }

                node.Next = new Node(item);
            }
            else
            {
                first = new Node(item);
            }

            Count++;
        }

        public T Get(int index)
        {
            var node = first;

            // dit is een grapje van Eric Lippert!
            while (index-- > 0)
            {
                node = node.Next;
            }

            return node.Item;
        }

        public bool Contains(IComparable<T> item)
        {
            if (first != null)
            {
                var node = first;
                while (node != null && item.CompareTo(node.Item) != 0)
                {
                    node = node.Next;
                }

                return node != null;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListEnumerator(first);
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

        class LinkedListEnumerator : IEnumerator<T>
        {
            Node current;

            public LinkedListEnumerator(Node first)
            {
                current = first;
            }

            public T Current => current.Item;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (current.Next != null)
                {
                    current = current.Next;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }
        }
    }
}