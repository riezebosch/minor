using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class StringExamples
    {
        [Fact]
        public void StringsAreImmutable()
        {
            var input1 = "Dit is een voorbeeld van een string";
            var input2 = input1.Replace("voorbeeld", "example");

            

            Assert.Equal("Dit is een example van een string", input1);
            Assert.Equal('t', input1[2]);

            input1 = input1 + "hoi";
            input1 += "hoi";
        }
    }
}
