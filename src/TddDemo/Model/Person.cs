using System;
using System.Collections.Generic;

namespace TddDemo.Model
{
    public partial class Person
    {
        public Person()
        {
            StudentGrade = new HashSet<StudentGrade>();
        }

        public int PersonId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? EnrollmentDate { get; set; }

        public virtual ICollection<StudentGrade> StudentGrade { get; set; }
        public DateTime BirthDate { get; internal set; }
    }
}
