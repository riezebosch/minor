using Xunit;

namespace TddDemo
{
    public class QueueTests
    {
        [Fact]
        public void GivenQueueWithSingleItemWhenPopThanLastItemShouldBeReturned()
        {
            int input = 1;
            var queue = new Queue<int>();
            queue.Enqueue(input);

            int result = queue.Dequeue();

            Assert.Equal(result, input);
        }

        [Fact]
        public void GivenStackWithSingleItemWhenPopThanLastItemShouldBeEmpty()
        {
            int input = 1;
            var queue = new Queue<int>();
            queue.Enqueue(input);

            queue.Dequeue();

            Assert.True(queue.IsEmpty());
        }
    }
}
