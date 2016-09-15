using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    class LinkedList<T>
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

        private class Node
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