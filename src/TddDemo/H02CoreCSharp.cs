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
        public void VanWelkTypeIsVar()
        {
            var v = 0;
        }

        int _d;

        [Fact]
        public void InitialisatieVanLokaleVariablen()
        {
            int d;

            //Console.WriteLine(d);
            Console.WriteLine(_d);
        }

        [Fact]
        public void DeScopeVanEenIterationVariable()
        {
            int j = 3;
            //for (int j = 0; j < 10; j++)
            {
            }

            //Console.WriteLine(j);
        }


        int j = 3;
        int _j = 3;

        [Fact]
        public void ScopeClashForFieldsAndLocalVariables()
        {
            int j = 5;
            Assert.Equal(5, j);
            Assert.Equal(3, this.j);
            Assert.Equal(3, _j);
        }

        [Fact]
        public void DoublePrecision()
        {
            Assert.False(0.1 + 0.2 == 0.3);
            Assert.True(0.1m + 0.2m == 0.3m);
            Assert.False(1m / 3m * 3m == 1m);
            Assert.True(1.0 / 3.0 * 3.0 == 1.0);
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

            Assert.Equal("Dutch", language);
        }
    }

    public class H03ObjectsAndTypes
    {
        [Fact]
        public void ParamsArray()
        {
            Print("a", "b", "c");
        }

        private void Print(params string[] args)
        {
            foreach (var item in args)
            {
                Console.Write(item);
            }
        }

        [Fact]
        public void StructsAndProperties()
        {
            // Dit mag niet omdat je anders een copy'tje zou aanpassen
            // Location.X = 3;

            // Dus moet het in 3 stappen
            Point m = this.Location;
            m.X = 3;
            Location = m;
        }


        private Point _location;

        // Even uitgeschreven om duidelijk te maken dat je vanuit
        // de getter een copy'tje van het field terugkrijg en niet
        // een referentie naar het field zelf.
        public Point Location
        {
            get { return _location; }
            set { _location = value; }
        }

        //public Point Location { get; set; }

        public struct Point
        {
            public int X;
            public int Y;
        }
    }
}