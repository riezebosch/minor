using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class StructDemo
    {
        struct MyStruct
        {

        }

        // Afleiden mag dus niet met structs!
        //struct MyStruct2 : MyStruct
        //{
        //}

        int i;

        [Fact]
        public void VerschilTussenClassEnStruct()
        {
            int i = 6;
            int j = i;

            i = 7;
            Assert.Equal(6, j);
        }

        [Fact]
        public void SindsCSharp6MagJeZelfEenDefaultConstructorMakenVoorStructs()
        {
            var pt = new Point(2, 2);

            Assert.Equal(2, pt.X);
            Assert.Equal(2, pt.Y);
        }

        struct Point
        {
            public int X { set; get; }
            public int Y;
            public Point(int x, int y) 
            {
                X = x;
                Y = y;
            }
        }
    }

}
