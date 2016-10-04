using System;
using System.Collections.Generic;

namespace TddDemo.Model
{
    public class Instructor : Person
    {
        public Instructor()
        {
            CourseInstructor = new HashSet<CourseInstructor>();
        }
        public DateTime? HireDate { get; set; }
        public virtual ICollection<CourseInstructor> CourseInstructor { get; set; }
        public virtual OfficeAssignment OfficeAssignment { get; set; }

    }
}