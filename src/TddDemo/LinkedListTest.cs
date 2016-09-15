﻿using System;
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

        [Fact]
        public void GivenAddedTwoItemsWhenContainsThenTrue()
        {
            // Arrange
            var list = new LinkedList<int>();
            list.Add(3);
            list.Add(5);

            // Act
            bool contains = list.Contains(5);

            // Assert
            Assert.True(contains);
        }

        [Fact]
        public void GivenAddedTwoItemsWhenContainsWithOTherThenFalse()
        {
            // Arrange
            var list = new LinkedList<int>();
            list.Add(3);
            list.Add(5);

            // Act
            bool contains = list.Contains(4);

            // Assert
            Assert.False(contains);
        }

        [Fact]
        public void IkZouGraagDoorMijnLijstWillenKunnenForeachen()
        {
            var list = new LinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            int sum = 0;

            foreach (var item in list)
            {
                sum += item;
            }

            Assert.Equal(6, sum);
        }
    }
}
