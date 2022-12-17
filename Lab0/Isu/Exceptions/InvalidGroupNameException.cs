using System;

namespace Isu.Exceptions
{
    public class InvalidGroupNameException : IsuException
    {
        public InvalidGroupNameException()
        {
            Console.WriteLine("There can't be such a group");
        }
    }
}
