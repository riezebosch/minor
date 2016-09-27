using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class LinqOefening
    {
        IEnumerable<string> plaatsnamen = new List<string>
        {
            "Amsterdam", "Arnhem", "Amersfoort",
            "Assen", "Amstelveen", "Alphen", "Zaandam"
        };

        [Fact]
        public void Opdracht1KortePlaatsnamen()
        {
            /* Opdracht 1;
             * Schrijf één LINQ-query (gebruikmakend van comprehension syntax / query syntax) 
             * die alle korte plaatsnamen (minder dan 8 letters), in volgorde van lengte, en 
             * bij gelijke lengte alfabetisch, oplevert.
             */

            var query = from p in plaatsnamen
                        where p.Length < 8
                        orderby p.Length, p
                        select p;
            Assert.Equal(new[] { "Assen", "Alphen", "Arnhem", "Zaandam" }, query);
        }

        [Fact]
        public void Opdracht2SomVanLengtes()
        {
            /* Opdracht 2. 
             * Schrijf één LINQ-query (gebruikmakend van extension methods / fluent syntax) 
             * die de som bepaalt van de lengtes van alle plaatsnamen die eindigen 
             * op een ‘m’. 
             * (Met één LINQ-query wordt hier een aaneengesloten reeks van extension methods / query operators bedoeld.)
             */

            var query = plaatsnamen.Where(p => p.EndsWith("m")).Sum(p => p.Length);
            Assert.Equal(22, query);
        }

        [Fact]
        public void Opdracht3MeestVoorkomendeEindletter()
        {
            /* Opdracht 3. 
             * Bepaal met behulp van LINQ de meest voorkomende eindletter 
             * van de plaatsnamen. Het antwoord is een lijstje dat bestaat uit één element 
             * als er precies één eindletter het vaakst voorkomt, en bestaat uit meerdere 
             * letters als er meerdere eindletters een eerste plaats delen. 
             * Je oplossing mag bestaan 
             * uit meerdere queries en/of LINQ-expressies.
             */

            var query = (from p in plaatsnamen
                         group p by p.Last() into g
                         group g.Key by g.Count() into g
                         orderby g.Key descending
                         select g).First();

            var anders = plaatsnamen.GroupBy(p => p.Last()).GroupBy(p => p.Count(), p => p.Key).OrderByDescending(p => p.Key).First();

            Assert.Equal(3, query.Key);
            Assert.Equal(new[] { 'm', 'n' }, query);
            Assert.Equal(query, anders);
        }
    }
}
