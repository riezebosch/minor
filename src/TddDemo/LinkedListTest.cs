using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class LinkedListTest
    {
        public int Count { get; private set; }

        [Fact]
        public void GivenEmptyListWhenAddThenCount1()
        {
            Add(3);
            Assert.Equal<int>(1, Count);
        }

        private void Add(int item)
        {
            Count++;
        }
    }
}
