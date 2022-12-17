using System;

namespace Isu.Exceptions
{
    public class InvalidCourseException : IsuException
    {
        public InvalidCourseException()
        {
            Console.WriteLine("There is no such course");
        }
    }
}
