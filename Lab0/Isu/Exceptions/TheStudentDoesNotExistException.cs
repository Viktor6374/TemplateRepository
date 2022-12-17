using System;

namespace Isu.Exceptions
{
    public class TheStudentDoesNotExistException : IsuException
    {
        public TheStudentDoesNotExistException()
        {
            Console.WriteLine("The student does not exist");
        }
    }
}
