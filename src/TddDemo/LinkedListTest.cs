using System;
using System.Collections.Generic;
using Xunit;

namespace TddDemo
{
    public class LinkedListTest
    {
        [Fact]
        public void GivenEmptyListWhenAddThenCount1()
        {
            // Arrange
            var list = new LinkedList<int>();

            // Act
            list.Add(3);

            // Assert
            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void GivenItemAddedItemWhenGetThenItemReturned()
        {
            // Arrange
            var list = new LinkedList<int>();
            list.Add(3);

            // Act
            var item = list.Get(0);

            // Assert
            Assert.Equal(3, item);
        }

        [Fact]
        public void GivenAddedTwoItemsWhenGetLastThenSecondItemIsReturned()
        {
            // Arrange
            var list = new LinkedList<object>();
            list.Add(3);
            list.Add(5);

            // Act
            var item = list.Get(1);

            // Assert
            Assert.Equal(5, item);
        }

        [Fact]
        public void GivenAddedTwoItemsWhenGetFirstThenFirstItemIsReturned()
        {
            // Arrange
            var list = new LinkedList<int>();
            list.Add(3);
            list.Add(5);

            // Act
            var item = list.Get(0);

            // Assert
            Assert.Equal(3, item);
        }

        [Fact]
        public void GivenAddedTwoItemsWhenContainsThenTrue()
        {
            // Arrange
            var list = new LinkedList<int>();
            list.Add(3);
            list.Add(5);

            // Act
            bool contains = list.Contains(5);

            // Assert
            Assert.True(contains);
        }

        [Fact]
        public void GivenAddedTwoItemsWhenContainsWithOTherThenFalse()
        {
            // Arrange
            var list = new LinkedList<int>();
            list.Add(3);
            list.Add(5);

            // Act
            bool contains = list.Contains(4);

            // Assert
            Assert.False(contains);
        }

        [Fact]
        public void ContainsMetStringsMaarDanOpPartialMatch()
        {
            // Arrange
            var list = new LinkedList<string>
            {
                "asdf asdfj;klasdf jlsdf",
                "qewrupq uiower uiower"
            };

            /* Gebruik één van deze twee interfaces (of allebei):
             *  IComparer<string>
             *  IEqualityComparer<string> 
             */
            IEqualityComparer<string> comparer = 
                new CustomStringComparer();
            
            // Act
            bool contains = list.Contains("asdfj;", comparer);

            // Assert
            Assert.True(contains);
        }

        

        [Fact]
        public void ContainsMetStringsMaarDanMetDelegates()
        {
            // Arrange
            var list = new LinkedList<string>
            {
                "asdf asdfj;klasdf jlsdf",
                "qewrupq uiower uiower"
            };

            // Act
            var method = new Equals<string>(Equals);
            bool contains = list.Contains("asdfj;", method);

            // Assert
            Assert.True(contains);
        }

        private bool Equals(string x, string y) => x.Contains(y);

        [Fact]
        public void IkZouGraagDoorMijnLijstWillenKunnenForeachen()
        {
            var list = new LinkedList<int> { 1, 2, 3 };
            int sum = 0;

            foreach (var item in list)
            {
                sum += item;
            }

            Assert.Equal(6, sum);
        }

        [Fact]
        public void Given2ListsWhenConcatenatingThenResultContainsItemsFromLists()
        {
            var lista = new LinkedList<int> { 1, 2, 3 };
            var listb = new LinkedList<int> { 7, 8, 9 };

            lista.AddRange(listb);
            Assert.True(lista.Contains(9));
        }

        [Fact]
        public void Given2ListsWithDifferentTypesWhenConcatenatingThenResultContainsItemsFromLists()
        {
            var hond = new Hond { Aaibaarheid = 9 };
            var lista = new LinkedList<Dier> { hond };
            var listb = new LinkedList<Hond> { hond };

            lista.AddRange(listb);
            Assert.Equal(2, lista.Count);
        }

        [Fact]
        public void Given2ListsWithDifferentValueTypesWhenConcatenatingThenResultContainsItemsFromLists()
        {
            var lista = new LinkedList<int> { 1 ,2 ,3 };
            var listb = new LinkedList<double> { 7, 8, 9 };

            // Dit mag dus niet omdat double niet is afgeleid van int!
            //lista.AddRange(listb);
        }

        [Fact]
        public void ContraVarianceMetTestVulMethode()
        {
            var lista = new LinkedList<Dier>();
            var listb = new LinkedList<Hond>();

            VulLijstMetHonden(lista);
            VulLijstMetHonden(listb);

            Assert.Equal(2, lista.Count);
            Assert.Equal(2, listb.Count);
        }

        void VulLijstMetHonden(IAdd<Hond> list)
        {
            list.Add(new Hond { Aaibaarheid = 9 });
            list.Add(new Hond { Aaibaarheid = 12 });
        }

        class Dier
        {
            public int AantalPoten { get; set; }
        }

        class Hond : Dier
        {
            public int Aaibaarheid { get; set; }
        }

        private class CustomStringComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return x.Contains(y);
            }

            public int GetHashCode(string obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}
