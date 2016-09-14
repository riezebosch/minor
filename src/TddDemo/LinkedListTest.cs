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

        class LinkedList
        {
            public int Count { get; private set; }

            public void Add(int item)
            {
                Count++;
            }
        }
    }
}
