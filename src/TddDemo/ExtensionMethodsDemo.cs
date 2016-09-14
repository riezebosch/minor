using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System.Reflection;

namespace TddDemo
{
    static class SomeClassExt
    {
        public static void Update(this SomeClass item, int value)
        {
            // Dit mag nu niet: item._i = value;
            typeof(SomeClass)
                .GetField("_i", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(item, value);
        }

        public static string ConvertToString(this DayOfWeek day)
        {
            return "alle dagen zijn goed";
        }
    }
    class SomeClass
    {
        private int _i;
        public SomeClass()
        {
        }

        public int I => _i;
    }

    public class ExtensionMethodsDemo
    {




        [Fact]
        public void WatZijnExtensionMethodsEigenlijk()
        {
            var s = new SomeClass();

            s.Update(3);
            SomeClassExt.Update(s, 3);

            Assert.Equal(3, s.I);

            DayOfWeek.Friday.ConvertToString();
        }
    }
}
