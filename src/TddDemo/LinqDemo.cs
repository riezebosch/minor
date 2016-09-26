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

    }
}
