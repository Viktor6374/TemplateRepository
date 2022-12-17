using System;

namespace Isu.Exceptions
{
    public class TheGroupDoesNotExistException : IsuException
    {
        public TheGroupDoesNotExistException()
        {
            Console.WriteLine("The Group Does Not Exist Exception");
        }
    }
}
