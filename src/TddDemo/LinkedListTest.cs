using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class LinkedListTest
    {
        [Fact]
        public void GivenEmptyListWhenAddThenCount1()
        {
            // Arrange
            var list = new LinkedList();

            // Act
            list.Add(3);

            // Assert
            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void GivenItemAddedItemWhenGetThenItemReturned()
        {
            // Arrange
            var list = new LinkedList();
            list.Add(3);

            // Act
            var item = list.Get(0);

            // Assert
            Assert.Equal(3, item);
        }

        [Fact]
        public void GivenAddedTwoItemsWhenGetLastThenSecondItemIsReturned()
        {
            // Arrange
            var list = new LinkedList();
            list.Add(3);
            list.Add(5);

            // Act
            var item = list.Get(1);

            // Assert
            Assert.Equal(5, item);
        }

        [Fact]
        public void GivenAddedTwoItemsWhenGetFirstThenFirstItemIsReturned()
        {
            // Arrange
            var list = new LinkedList();
            list.Add(3);
            list.Add(5);

            // Act
            var item = list.Get(0);

            // Assert
            Assert.Equal(3, item);
        }

        class LinkedList
        {
            private Node first;

            public int Count { get; private set; }

            public void Add(int item)
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

            public object Get(int index)
            {
                var node = first;

                // dit is een grapje van Eric Lippert!
                while (index --> 0)
                {
                    node = node.Next;
                }

                return node.Item;
            }

            private class Node
            {
                public Node(int item)
                {
                    this.Item = item;
                }

                public object Item { get; }
                public Node Next { get; set; }
            }
        }
    }
}
