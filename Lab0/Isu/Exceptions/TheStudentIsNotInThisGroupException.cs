using System;

namespace Isu.Exceptions
{
    public class TheStudentIsNotInThisGroupException : IsuException
    {
        public TheStudentIsNotInThisGroupException()
        {
            Console.WriteLine("The student is not in this group");
        }
    }
}
