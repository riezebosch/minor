using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class OperatorDemo
    {
        [Fact]
        public void NullPropagationOperator()
        {
            string s = "abc";
            int lengte = BerekenDeLengte(s);

            Assert.Equal(3, lengte);
            Assert.Equal(0, BerekenDeLengte(null));
        }

        private int BerekenDeLengte(string s)
        {
            return s?.Length ?? 0;
            return s != null ? s.Length : 0;
        }

        [Fact]
        public void ModuloRekenen()
        {
            var result = 3 % 4;
            Assert.Equal(3, result);
        }

        [Fact]
        public void IncrementOperator()
        {
            int a = 10;
            int b = 10;

            a++;
            ++b;

            Assert.Equal(a, b);

            Assert.Equal(11, a++);
            Assert.Equal(12, a);

            Assert.Equal(12, ++b);
            Assert.Equal(12, b);
        }

        [Fact]
        public void ZonderCheckedOperator()
        {
            int heelgroot = int.MaxValue;
            heelgroot++;

            Assert.Equal(int.MinValue, heelgroot);
        }

        [Fact]
        public void CheckedOperator()
        {
            int heelgroot = int.MaxValue;

            checked
            {
                Assert.Throws<OverflowException>(() => heelgroot++); 
            }
        }

        [Fact]
        public void CheckedOperatorMetCastVanDouble()
        {
            double heelgroot = 1d + int.MaxValue;

            checked
            {
                Assert.Throws<OverflowException>(() => (int)heelgroot);
            }
        }

        [Fact]
        public void CheckedOperatorNietNodigMetDecimals()
        {
            var heelgroot = decimal.MaxValue;
            Assert.Throws<OverflowException>(() => heelgroot++);
        }

        [Fact]
        public void CastVanIntNaarFloatEnWeerTerug()
        {
            int i = int.MaxValue;
            float f = float.MaxValue;

            float fresult = i;
            int   iresult = (int)f;

            Assert.Equal(sizeof(int), sizeof(float));
            Assert.Equal(i, fresult);
        }

        [Fact]
        public void ErZittenGatenInDeFloat()
        {
            float f = 1;
            while (f != f + 1)
            {
                f *= 2;
            }

            Assert.Equal(f, f + 1);
        }

        [Fact]
        public void ZittenErOokGatenInDeDecimal()
        {
            decimal f = 1;
            while (f != f + 1 && f < decimal.MaxValue / 2)
            {
                f *= 2;
            }

            // Geen waterdicht bewijs, maar niet gevonden
            Assert.NotEqual(f, f + 1); 
        }

        [Fact]
        public void StringsLijkenNetArrays()
        {
            string input = "abcd";
            Assert.Equal('a', input[0]);
        }
    }
}
