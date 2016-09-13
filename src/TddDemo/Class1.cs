using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class H02CoreCSharp
    {
        [Fact]
        public void DoublePrecision()
        {
            Assert.True(0.1 + 0.2 == 0.3);
        }

        [Fact]
        public void SwitchStatement()
        {
            string country = "NL";
            string language;

            switch (country)
            {
                default:
                    language = "undefined";
                    break;

                case "NL":
                case "BE":
                    language = "Dutch";
                    break;


                case "US":
                case "UK":
                    language = "English";
                    break;
            }
        }
    }
}
