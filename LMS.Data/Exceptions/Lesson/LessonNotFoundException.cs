using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Data.Exceptions.Lesson
{
    public class LessonNotFoundException : Exception
    {
        public LessonNotFoundException() : base("Lesson Not Found")
        {

        }
    }
}
