using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Data.Exceptions.Course
{
    public class SameCourseException : Exception
    {
        public SameCourseException(string message) : base(message) { }

    }
}
