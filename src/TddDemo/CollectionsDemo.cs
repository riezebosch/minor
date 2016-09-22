using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class CollectionsDemo
    {
        [Fact]
        public void DictionaryDemo()
        {
            var dict = new Dictionary<string, string>();
            dict["gladiool"] = "iemand die zich niet gedraagt conform sociaal wenselijk afgestemde normen en waarden";

            var betekenis = dict["gladiool"];
            Assert.Contains("sociaal", betekenis);

            var personeel = new Dictionary<int, Persoon>
            {
                //Oud:
                // { 202060, new Persoon { Naam = "Pietje Puk", Functie = "Postbezorger" } }
                // Nieuw in C# 6
                   [202060] = new Persoon { Naam = "Pietje Puk", Functie = "Postbezorger" }
            };

            personeel[1234] = new Persoon { Naam = "Langraad", Functie = "Agent" };
            Assert.Equal("Pietje Puk", personeel[202060].Naam);
        }

        [Fact]
        public void ToLookUp()
        {
            int[] items = { 1, 2, 3, 4 };
            var lookup = items.ToLookup(i => i % 2);
            
            Assert.Equal(new[] { 2, 4 }, lookup[0]);
            Assert.Equal(new[] { 1, 3 }, lookup[1]);

            Assert.Throws<ArgumentException>(() => 
                items.ToDictionary(i => i % 2));
            
        }

        private class Persoon
        {
            public string Functie { get; internal set; }
            public string Naam { get; internal set; }
        }
    }
}
