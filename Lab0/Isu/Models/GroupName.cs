using Isu.Exceptions;

namespace Isu.Models
{
    public class GroupName
    {
        private readonly int minNumberInGroupName = 1100;
        private readonly int maxNumberInGroupName = 94999;
        public GroupName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidGroupNameException();
            }

            CourseNumber = new CourseNumber(Validation(name));
            Name = name;
        }

        public string Name { get; }
        public CourseNumber CourseNumber { get; }
        private int Validation(string name)
        {
            int number;
            if (!int.TryParse(name.Substring(1, name.Length - 1), out number))
            {
                throw new InvalidGroupNameException();
            }

            if (number < minNumberInGroupName && number > maxNumberInGroupName)
            {
                throw new InvalidGroupNameException();
            }

            int course = number % 10;
            while (course > 9)
                course /= 10;
            return course;
        }
    }
}