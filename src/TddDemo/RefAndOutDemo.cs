using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class RefAndOutDemo
    {
        [Fact]
        public void PassByRef()
        {
            int x = 3;

            Update(x);
            Assert.Equal(3, x);

            Update(ref x);
            Assert.Equal(5, x);

            var pien = new Hond { Aaibaarheid = 9 };
            Update(pien);
            Assert.Equal(9, pien.Aaibaarheid);

            Update(ref pien);
            Assert.Equal(1000, pien.Aaibaarheid);
        }

        private void Update(Hond hond)
        {
            hond = new Hond { Aaibaarheid = 1000 };
            hond.Aaibaarheid++;
        }

        private void Update(ref Hond hond)
        {
            hond = new Hond { Aaibaarheid = 1000 };
        }

        private void Update(int x)
        {
            x = 5;
        }

        private void Update(ref int x)
        {
            x = 5;
        }

        class Hond
        {
            public int Aaibaarheid { get; set; }
        }

        [Fact]
        public void WatIsHetVerschilTussenOutEnRef()
        {
            int x = 3;
            int y;

            Update(ref x);
            Assert.Equal(5, x);

            UpdateWithOut(out y);
            Assert.Equal(6, y);


            // Praktijkvoorbeeld van een out-parameter:
            string value;
            var dict = new Dictionary<int, string>();
            dict.TryGetValue(23, out value);
        }

        private void UpdateWithOut(out int y)
        {
            y = 6;
        }
    }
}
