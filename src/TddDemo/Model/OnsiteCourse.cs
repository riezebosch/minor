using System;
using System.Collections.Generic;

namespace TddDemo.Model
{
    public partial class OnsiteCourse : Course
    {
        public string Location { get; set; }
        public string Days { get; set; }
        public DateTime Time { get; set; }
    }
}
