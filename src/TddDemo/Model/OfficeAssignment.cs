using System;
using System.Collections.Generic;

namespace TddDemo.Model
{
    public partial class OfficeAssignment
    {
        public int InstructorId { get; set; }
        public string Location { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Instructor Instructor { get; set; }
    }
}
