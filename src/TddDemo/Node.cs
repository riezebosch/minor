using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    class Node<T>
    {
        public Node(T item)
        {
            this.Item = item;
        }

        public T Item { get; set; }
        public Node<T> Next { get; set; }

        public static Node<T> operator +(Node<T> current, Node<T> next)
        {
            return current.Next = next;
        }

        public static Node<T> operator ++(Node<T> node)
        {
            return node.Next;
        }

        public static implicit operator Node<T>(T item)
        {
            return new Node<T>(item);
        }

        public static implicit operator T(Node<T> node)
        {
            return node.Item;
        }
    }
}