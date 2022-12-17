using System;

namespace Isu.Extra.Exceptions
{
    public class CourseNumberException : Exception
    {
        public CourseNumberException(string massage)
            : base(massage) { }
        public static CourseNumberException InvalidCourse()
        {
            return new CourseNumberException($"The course should have a value from 1 to 4");
        }
    }
}
