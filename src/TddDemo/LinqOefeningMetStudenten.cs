using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TddDemo
{
    public class LinqOefeningMetStudenten
    {
        [Fact]
        public void HoeWerktAssertMetSingleObjectEquality()
        {
            var student = new Student { Naam = "Pieter", Nummer = 1 };
            var result = new Student { Naam = "Pieter", Nummer = 1 };

            Assert.Equal(student, result, new StudentComparer());
        }

        private class Student
        {
            public string Naam { get; set; }
            public int Nummer { get; set; }
        }

        private class StudentComparer : IEqualityComparer<Student>
        {
            public bool Equals(Student x, Student y)
            {
                return x.Naam == y.Naam &&
                    x.Nummer == y.Nummer;
            }

            public int GetHashCode(Student obj)
            {
                throw new NotImplementedException();
            }
        }
    }


}
