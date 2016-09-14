using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class MethodOverloadingDemo
    {
        [Fact]
        public void MethodOverloadedAanroep()
        {
            Test();
            Test(3);
        }

        //private void Test()
        //{
        //}

        private int Test(int a = 12) => 3 * a;

        [Fact]
        public void ConstructorDemo()
        {
            var item = new Item(12);
        }

        [Fact]
        public void StaticConstructorDemo()
        {
            //new Item();
            //new Item();
            //new Item();

            Assert.Equal(1, Item.i);
        }
        class Item
        {
            public static int i;

            static Item()
            {
                i++;
            }

            public Item(int waarde)
            {
            }

            public Item() : this(0)
            {
            }
        }
    }

}
