using System;

namespace Isu.Extra.Exceptions
{
    public class StudyGroupException : Exception
    {
        public StudyGroupException(string massage)
            : base(massage) { }
        public static StudyGroupException CrowdedGroup()
        {
            return new StudyGroupException($"The group is crowded");
        }

        public static StudyGroupException TheGroupAlreadyExists()
        {
            return new StudyGroupException($"The group already exists");
        }

        public static StudyGroupException NullArgument()
        {
            return new StudyGroupException($"The argument cannot be Null");
        }

        public static StudyGroupException InvalidTimetable()
        {
            return new StudyGroupException($"Not all pairs belong to this group");
        }

        public static StudyGroupException InvalidFlow()
        {
            return new StudyGroupException($"There is no such flow in the course");
        }
    }
}
