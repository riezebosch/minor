using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class NullableTypes
    {
        [Fact]
        public void CastVanNullableTypes()
        {
            int x1 = 1;
            int? x2 = null;

            int x3 = x1;
            int x4 = (int)x1;
            // --> dit mag dus niet: int x5 = x2;
            // --> dit geeft runtime een fout: int x6 = (int)x2;
            

            int? x7 = x1;
            int? x8 = (int?)x1;
            int? x9 = x2;

            int x10 = x2 ?? 0;
            Assert.Equal(0, x10);
        }
    }
}
