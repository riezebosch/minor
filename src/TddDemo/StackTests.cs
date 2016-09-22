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
    }
}
