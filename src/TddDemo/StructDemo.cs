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
    }
}
