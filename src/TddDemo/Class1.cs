using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class Class1
    {
        [Fact]
        public void MijnEersteTestMethode()
        {
            // Arrange
            var geboorteDatum = new DateTime(1992, 10, 26);
            var huidig = new DateTime(2016, 09, 12);

            // Act
            var leeftijd = BerekenLeeftijd(geboorteDatum, huidig);

            // Assert
            Assert.Equal(23, leeftijd);
        }

        [Fact]
        public void EenAndereDatum()
        {
            // Arrange
            var geboorteDatum = new DateTime(1991, 12, 31);
            var huidig = new DateTime(2016, 09, 12);

            // Act
            var leeftijd = BerekenLeeftijd(geboorteDatum, huidig);

            // Assert
            Assert.Equal(24, leeftijd);
        }

        [Fact]
        public void GeboorteDatumInDeToekomstGeeftFoutmelding()
        {
            // Arrange
            var geboorteDatum = new DateTime(2017, 12, 31);
            var huidig = new DateTime(2016, 09, 12);

            // Act + Assert
            Assert.Throws(typeof(ArgumentException) , () => BerekenLeeftijd(geboorteDatum, huidig));
        }

        [Fact]
        public void WatAlsIemandNogNietJarigIsGeweest()
        {
            // Arrange
            var geboorteDatum = new DateTime(2016, 09, 11);
            var huidig = new DateTime(2016, 09, 12);

            // Act
            var leeftijd = BerekenLeeftijd(geboorteDatum, huidig);

            // Assert
            Assert.Equal(0, leeftijd);
        }

        private int BerekenLeeftijd(DateTime geboorteDatum, DateTime huidig)
        {
            if (geboorteDatum > huidig)
            {
                throw new ArgumentException($"Geboortedatum {geboorteDatum} moet kleiner zijn dan {huidig}.", 
                    nameof(geboorteDatum));
            }

            var result = huidig.Year - geboorteDatum.Year;
            if (geboorteDatum.Month != huidig.Month || geboorteDatum.Day >= huidig.Day)
            {
                result--;
            }

            return result;
        }
    }
}
