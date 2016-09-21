using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class NodeTests
    {
        [Fact]
        public void OperatorOpNodeClass()
        {
            var a = new Node<int>(1);
            var b = new Node<int>(2);
            var c = a + b;

            Assert.Equal(b, a.Next);
            Assert.Equal(b, c);
        }

        [Fact]
        public void MoveNextWithPlusPlusOperator()
        {
            var a = new Node<int>(1);
            var b = new Node<int>(2);
            var c = a + b;

            a++;
            Assert.Equal(b, a);
        }

        [Fact]
        public void CastItemToNode()
        {
            Node<int> a = 1;
            Assert.Equal(1, a.Item);
        }

        [Fact]
        public void CastNodeToItem()
        {
            var node = new Node<int>(2);
            int value = node;

            Assert.Equal(2, value);
        }
    }
}
