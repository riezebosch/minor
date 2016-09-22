using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class CollectionsDemo
    {
        [Fact]
        public void ToLookUp()
        {
            int[] items = { 1, 2, 3, 4 };
            var result = items.ToLookup(i => i % 2 == 0);
            
            Assert.Equal(new[] { 2, 4 }, result[true]);
            Assert.Equal(new[] { 1, 3 }, result[false]);
        }
    }
}
