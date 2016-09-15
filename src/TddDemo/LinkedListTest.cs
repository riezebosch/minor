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
            var list = new LinkedList<int>();

            // Act
            list.Add(3);

            // Assert
            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void GivenItemAddedItemWhenGetThenItemReturned()
        {
            // Arrange
            var list = new LinkedList<int>();
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
            var list = new LinkedList<object>();
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
            var list = new LinkedList<int>();
            list.Add(3);
            list.Add(5);

            // Act
            var item = list.Get(0);

            // Assert
            Assert.Equal(3, item);
        }
    }
}
