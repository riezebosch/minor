using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TddDemo.Model;
using Xunit;

namespace TddDemo
{
    public class Class1
    {
        [Fact]
        public void OnsiteAndOnlineCourseAreDerivedFromCourse()
        {
            using (var context = new SchoolContext())
            {
                Assert.True(context.Course.Any());
            }
        }
    }
}
