using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class LinqDemo
    {
        [Fact]
        public void WatIsEenQuery()
        {
            var players = new List<Person>
            {
                new Person
                {
                    Name = "Maarten",
                    Age = 36
                },
                new Person
                {
                    Name = "Pieter",
                    Age = 25
                }
            };

            IEnumerable<Person> query = from p in players
                        where p.Name.StartsWith("M")
                        orderby p.Age descending
                        select p;

            var other = players
                            .Where(p => p.Name.StartsWith("M"))
                            .OrderBy(p => p.Age);


            Assert.Equal(1, query.Count());

            players.Add(new Person
            {
                Name = "Mick",
                Age = 21
            });
            Assert.Equal(2, query.Count());

            query = from p in query
                    where p.Age < 26
                    select p;

            players.Add(new Person
            {
                Name = "Mike",
                Age = 19
            });

            Assert.Equal(2, query.Count());
        }

        private class Person
        {
            public int Age { get; internal set; }
            public List<Dier> Huisdieren { get; internal set; }
            public string Name { get; internal set; }
        }

        [Fact]
        public void HoeWerktLinq()
        {
            var players = new List<Person>
            {
                new Person
                {
                    Name = "Maarten",
                    Age = 36
                },
                new Person
                {
                    Name = "Pieter",
                    Age = 25
                }
            };

            var query = from p in players
                        where p.Name.StartsWith("M")
                        where p.Age < 25
                        orderby p.Age descending
                        select p;

            var other = players
                .Where(p => p.Name.StartsWith("M"))
                .Where(p => p.Age < 25)
                .OrderByDescending(p => p.Age)
                .Select(p => p);

            var hardcore = Enumerable.Where(
                Enumerable.Where(
                    Enumerable.OrderByDescending(
                        Enumerable.Select(players, 
                        p => p), p => p.Age), 
                    p => p.Age < 25), 
                p => p.Name.StartsWith("M"));

            Assert.Equal(query, other);
            Assert.Equal(query, hardcore);
        }


        [Fact]
        public void WatLevertDezeQueryOp()
        {
            var personen = new List<Person>
            {
                new Person
                {
                    Name = "Maarten",
                    Age = 36
                },
                new Person
                {
                    Name = "piet",
                    Age = 25
                }
            };

            var querysyntax = from p in personen
                              //where ...
                              select p.Name == "piet";

            var query = personen
                .Select(p => p.Name == "piet");

            Assert.Equal(new[] { false, true }, query);
        }

        [Fact]
        public void SelectVsSelectMany()
        {
            var personen = new List<Person>
            {
                new Person
                {
                    Name = "Maarten",
                    Age = 36,
                    Huisdieren = new List<Dier>
                    {
                        new Dier { Naam = "Flappie" }
                    }
                },
                new Person
                {
                    Name = "piet",
                    Age = 25,
                    Huisdieren = new List<Dier>
                    {
                        new Dier { Naam = "Nemo" },
                        new Dier { Naam = "Bluppie" }
                    }
                }
            };

            var dieren = personen
                .SelectMany(p => p.Huisdieren, 
                    (p, d) => d.Naam);

            var querysyntax = from p in personen
                              from d in p.Huisdieren
                              select d.Naam;

            Assert.Equal(new[] { "Flappie", "Nemo", "Bluppie" }, dieren);


            int[] numbers = { 1, 2, 3 };
            char[] characters = { 'A', 'B', 'C' };

            var cartesianproduct = from n in numbers
                                   from c in characters
                                   select $"({n},{c})";

            Assert.Equal(new[]
            {
                "(1,A)",
                "(1,B)",
                "(1,C)",
                "(2,A)",
                "(2,B)",
                "(2,C)",
                "(3,A)",
                "(3,B)",
                "(3,C)"
            }, cartesianproduct);
        }

        [Fact]
        public void VerschilTussenCastEnOfType()
        {
            object[] items = { new Person { }, new Dier { }, "woord", 3 , new Hond { } };
            var people = items.OfType<Person>();
            Assert.Equal(1, people.Count());

            var animals = items.OfType<Dier>();
            Assert.Equal(2, animals.Count());

            Assert.Throws<InvalidCastException>(() => items.Cast<Person>().ToList());
        }

        [Fact]
        public void OrderByEnThenBy()
        {
            var maarten = new Person { Name = "Maarten", Age = 17 };
            var pieter25 = new Person { Name = "Pieter", Age = 25 };
            var pieter18 = new Person { Name = "Pieter", Age = 18 };

            var personen = new List<Person>
            {
                maarten,
                pieter25,
                pieter18
            };

            var query = personen.OrderBy(p => p.Name).ThenBy(p => p.Age);
            Assert.Equal(new[] { maarten, pieter18, pieter25 }, query);

            var querysyntax = from p in personen
                              orderby p.Name, p.Age
                              select p;
        }
    }

    internal class Hond : Dier
    {
    }

    internal class Dier
    {
        public string Naam { get; internal set; }
    }
}
