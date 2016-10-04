using System;
using System.Collections.Generic;

namespace TddDemo.Model
{
    public partial class CourseInstructor
    {
        public int CourseId { get; set; }
        public int PersonId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}
