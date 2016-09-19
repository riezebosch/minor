using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class GenericsDemo
    {
        [Fact]
        public void WerktDeArrayListNogVandaag()
        {
            // Dit kan, maar dan heb ik wel een extra package nodig
            //var list = new ArrayList
        }

        class GenericClassDemo<T>
        {
            public static int Teller;
            public GenericClassDemo()
            {
                Teller++;
            }
        }

        [Fact]
        public void HoeveelTypesVoorEenGenericType()
        {
            new GenericClassDemo<int>();
            new GenericClassDemo<int>();
            new GenericClassDemo<string>();

            Assert.Equal(2, GenericClassDemo<int>.Teller);
            Assert.Equal(1, GenericClassDemo<string>.Teller);
            Assert.Equal(0, GenericClassDemo<object>.Teller);
        }

        [Fact]
        public void GenericReturnType()
        {
            var o = Initialize<int>();
        }

        private T Initialize<T>()
        {
            return default(T);
        }

        [Fact]
        public void GenericConstraintsMetMeerdereInterfaces()
        {
            //DoeMeerdereDingen(6);
        }

        private void DoeMeerdereDingen<T>(T input)
            where T : IComparer<T>,
            IEqualityComparer<T>,
            IDisposable,
            ICustomFormatter,
            IAdd<T>
        {
            input.Add(default(T));
        }
    }
}
