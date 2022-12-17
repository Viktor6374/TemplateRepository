using System;

namespace Isu.Extra.Exceptions
{
    public class InvalidArgumentException : Exception
    {
        public InvalidArgumentException(string massage)
            : base(massage) { }
        public static InvalidArgumentException NullArgument()
        {
            return new InvalidArgumentException($"this field cannot have a Null value");
        }

        public static InvalidArgumentException IncorrectValue()
        {
            return new InvalidArgumentException($"This value is not allowed");
        }
    }
}
