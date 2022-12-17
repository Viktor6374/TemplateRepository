using System;

namespace Isu.Extra.Exceptions
{
    public class GroupNameException : Exception
    {
        public GroupNameException(string massage)
            : base(massage) { }
        public static GroupNameException InvalidGroupName()
        {
            return new GroupNameException($"Invalid group name");
        }
    }
}
