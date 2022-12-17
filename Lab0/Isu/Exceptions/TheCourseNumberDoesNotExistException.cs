using System;

namespace Isu.Exceptions
{
    public class TheCourseNumberDoesNotExistException : IsuException
    {
        public TheCourseNumberDoesNotExistException()
        {
            Console.WriteLine("The Group Does Not Exist Exception");
        }
    }
}
