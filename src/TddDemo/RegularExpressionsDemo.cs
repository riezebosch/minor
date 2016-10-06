using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class RegularExpressionsDemo
    {
        [Fact]
        public void WatIsEenRegularExpression()
        {
            string input = "DitIsInput";
            string pattern = "DitIsInput";

            Assert.True(Regex.IsMatch(input, pattern));
        }

        [Fact]
        public void WatAlsHijHalverwegeVoorkomt()
        {
            string input1 = "DitIsInput";
            string input2 = "SomeLeadingDataDitIsInputSomeTrailing";
            string pattern = "^DitIsInput";

            Assert.True(Regex.IsMatch(input1, pattern));
            Assert.False(Regex.IsMatch(input2, pattern));
        }

        [Fact]
        public void WatAlsHijAanHetBeginWelVoorkomtMaarVerderopNietMeerGelijkIs()
        {
            string input1 = "DitIsInput";
            string input2 = "DitIsInputSomeTrailing";
            string pattern = "^DitIsInput$";

            Assert.True(Regex.IsMatch(input1, pattern));
            Assert.False(Regex.IsMatch(input2, pattern));
        }

        [Fact]
        public void WatAlsErMeerdereKaraktersGoedZijnOpEenPlek()
        {
            string input1 = "DitIsInput";
            string input2 = "DitIsInbut";
            string pattern = "^DitIsIn[pb]ut$";

            Assert.True(Regex.IsMatch(input1, pattern));
            Assert.True(Regex.IsMatch(input2, pattern));
        }

        [Fact]
        public void WatNouAlsEenKarakterOptioneelIs()
        {
            string input1 = "DitIsInput";
            string input2 = "DitIsInut";
            string pattern = "^DitIsIn[pb]?ut$";

            Assert.True(Regex.IsMatch(input1, pattern));
            Assert.True(Regex.IsMatch(input2, pattern));
        }

        [Fact]
        public void WatNouAlsEenKarakter0OfMeerKeerVoorMagKomen()
        {
            string input1 = "DitIsInput";
            string input2 = "DitIsInpbpbpbpbpbpbpbut";
            string input3 = "DitIsInut";
            string pattern = "^DitIsIn[pb]*ut$";

            Assert.True(Regex.IsMatch(input1, pattern));
            Assert.True(Regex.IsMatch(input2, pattern));
            Assert.True(Regex.IsMatch(input3, pattern));
        }

        [Fact]
        public void WatNouAlsEenKarakter1OfMeerKeerVoorMagKomen()
        {
            string input1 = "DitIsInput";
            string input2 = "DitIsInpbpbpbpbpbpbpbut";
            string input3 = "DitIsInut";
            string pattern = "^DitIsIn[pb]+ut$";

            Assert.True(Regex.IsMatch(input1, pattern));
            Assert.True(Regex.IsMatch(input2, pattern));
            Assert.False(Regex.IsMatch(input3, pattern));
        }

        [Fact]
        public void WatNouAlsIkEenGroepjeVanKaraktersOptioneelWilMaken()
        {
            string input1 = "DitIsInput";
            string input2 = "DitIs";
            string input3 = "DitIsInpu";
            string pattern = "^DitIs(Input)?$";

            Assert.True(Regex.IsMatch(input1, pattern));
            Assert.True(Regex.IsMatch(input2, pattern));
            Assert.False(Regex.IsMatch(input3, pattern));
        }

        [Fact]
        public void WatNouAlsIkOpEenBepaaldePlekAlleenEenGetalVerwacht()
        {
            string input1 = "DitIsInput";
            string input2 = "DitIsInput3";
            string input3 = "DitIsInputk";
            string input4 = "DitIsInput١";
            string pattern = @"^DitIsInput\d?$";

            Assert.True(Regex.IsMatch(input1, pattern));
            Assert.True(Regex.IsMatch(input2, pattern));
            Assert.False(Regex.IsMatch(input3, pattern));
            Assert.True(Regex.IsMatch(input4, pattern));
        }

        [Fact]
        public void WatNouAlsIkOpEenBepaaldePlekAlleenGeenGetalVerwacht()
        {
            string input1 = "DitIsInput";
            string input2 = "DitIsInputk";
            string input3 = "DitIsInput3";
            string pattern = @"^DitIsInput\D?$";

            Assert.True(Regex.IsMatch(input1, pattern));
            Assert.True(Regex.IsMatch(input2, pattern));
            Assert.False(Regex.IsMatch(input3, pattern));
        }

        [Fact]
        public void WatAlsIkTweeOptiesHebHetLiefstNogGroepjesOok()
        {
            string input1 = "DitIsInput";
            string input2 = "DitIsOutput";
            string input3 = "DitIsInpout";
            string pattern = @"^DitIs(In|Out)put$";

            Assert.True(Regex.IsMatch(input1, pattern));
            Assert.True(Regex.IsMatch(input2, pattern));
            Assert.False(Regex.IsMatch(input3, pattern));
        }

        [Fact]
        public void WatAlsIkEenSpecifiekeGroepPerSeNietWil()
        {
            string input1 = "DitIsInput";
            string input2 = "DitIsImput";
            string input3 = "DitIsIlput";
            string pattern = @"^DitIsI[^l]put$";

            Assert.True(Regex.IsMatch(input1, pattern));
            Assert.True(Regex.IsMatch(input2, pattern));
            Assert.False(Regex.IsMatch(input3, pattern));
        }

        [Fact]
        public void TerugVerwijzenNaarEenVorigeGroep()
        {
            string input1 = "DitIsInput";
            string input2 = "DitIsASDFputASDF";
            string input3 = "DitIsASDFputQWE";
            string pattern = @"^DitIs(.*)put\1?$";

            Assert.True(Regex.IsMatch(input1, pattern));
            Assert.True(Regex.IsMatch(input2, pattern));
            Assert.False(Regex.IsMatch(input3, pattern));
        }

        [Fact]
        public void WatAlsIkDrieKeerHetzelfdeWil()
        {
            string input1 = "DitIsInput";
            string input2 = "DitIsInnnput";
            string input3 = "DitIsInnnnput";
            string pattern = @"^DitIsIn{1,3}put$";

            Assert.True(Regex.IsMatch(input1, pattern));
            Assert.True(Regex.IsMatch(input2, pattern));
            Assert.False(Regex.IsMatch(input3, pattern));
        }

        [Fact]
        public void WatAlsIkHetMinimaalDrieKeerWil()
        {
            string input1 = "DitIsInnnput";
            string input2 = "DitIsInnnnnnnput";
            string input3 = "DitIsInnput";
            string pattern = @"^DitIsIn{3,}put$";

            Assert.True(Regex.IsMatch(input1, pattern));
            Assert.True(Regex.IsMatch(input2, pattern));
            Assert.False(Regex.IsMatch(input3, pattern));
        }

        [Fact]
        public void WatAlsIkHetPrecies3KeerWil()
        {
             string input1 = "DitIsInnnput";
            string input2 = "DitIsInnnnput";
            string input3 = "DitIsInnput";
            string pattern = @"^DitIsIn{3}put$";

            Assert.True(Regex.IsMatch(input1, pattern));
            Assert.False(Regex.IsMatch(input2, pattern));
            Assert.False(Regex.IsMatch(input3, pattern));
        }
    }
}
