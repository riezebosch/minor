using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class FlagsEnumDemo
    {
        [Fact]
        public void WatDoetEenFlagsAttributeOpEenEnum()
        {
            var dag = Dagen.Maandag;

            Assert.Equal(1, (int)dag);
            Assert.Equal(Dagen.Maandag, dag);

            dag = Dagen.Dinsdag;
            Assert.Equal(2, (int)dag);
            Assert.Equal(Dagen.Dinsdag, dag);

            var aanwezig = Dagen.Maandag | 
                Dagen.Dinsdag | 
                Dagen.Woensdag | 
                Dagen.Donderdag;

            Assert.Equal(15, (int)aanwezig);

            var benikopdonderdagaanwezig = (aanwezig & Dagen.Donderdag) == Dagen.Donderdag;
            Assert.True(benikopdonderdagaanwezig);

            // NIEUW! Makkelijker om flag te controleren
            Assert.True(aanwezig.HasFlag(Dagen.Donderdag));

            var printable = aanwezig.ToString();
            Assert.Equal("Maandag, Dinsdag, Woensdag, Donderdag", printable);

            var weekend = Dagen.Zaterdag | Dagen.Zondag;
            Assert.Equal("Weekend", weekend.ToString());
        }

        [Flags]
        enum Dagen
        {
            Maandag = 1,
            Dinsdag = 2,
            Woensdag = 4,
            Donderdag = 8,
            Vrijdag = 16,
            Zaterdag = 32,
            Zondag = 64,
            Weekend = Zaterdag | Zondag
        }
    }
}

