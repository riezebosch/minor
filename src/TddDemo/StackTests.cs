using System;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace TddDemo
{
    public class StackTests
    {
        [Fact]
        public void GivenStackWithSingleItemWhenPopThanLastItemShouldBeReturned()
        {
            int input = 1;
            var stack = new Stack<int>();
            stack.Put(input);

            int result = stack.Pop();

            Assert.Equal(result, input);
        }

        [Fact]
        public void GivenStackWithSingleItemWhenPopThanLastItemShouldBeEmpty()
        {
            int input = 1;
            var stack = new Stack<int>();
            stack.Put(input);

            stack.Pop();

            Assert.True(stack.IsEmpty());
        }

        [Fact]
        public void PerformanceVanEenStack()
        {
            var items = new Stack<int>();
            var sw = Stopwatch.StartNew();

            Enumerable
                .Range(0, 100000)
                .ToList()
                .ForEach(i => items.Put(i));

            while (!items.IsEmpty())
            {
                items.Pop();
            }

            Assert.True(sw.Elapsed < TimeSpan.FromSeconds(60));
        }

        [Fact]
        public void PerformanceVanEenQueue()
        {
            var items = new Queue<int>();
            var sw = Stopwatch.StartNew();

            Enumerable
                .Range(0, 100000)
                .ToList()
                .ForEach(i => items.Enqueue(i));

            while (!items.IsEmpty())
            {
                items.Dequeue();
            }

            Assert.True(sw.Elapsed < TimeSpan.FromSeconds(60));
        }
    }
}
