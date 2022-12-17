using System;

namespace Isu.Extra.Exceptions
{
    public class StudentException : Exception
    {
        public StudentException(string massage)
            : base(massage) { }
        public static StudentException InvalidStudentName()
        {
            return new StudentException($"A student cannot have such a name");
        }

        public static StudentException TheStudentDoesNotExistException()
        {
            return new StudentException($"The student does not exist exception");
        }

        public static StudentException TheStudentIsAlreadyInTheGroupException()
        {
            return new StudentException($"The student is already in the group exception");
        }

        public static StudentException TheStudentIsNotInThisGroupException()
        {
            return new StudentException($"The student is not in this group exception");
        }

        public static StudentException OverflowCombinedGroupTrainingAreas()
        {
            return new StudentException($"Combined group training areas can't be more than 2");
        }

        public static StudentException CourseIsAlreadyThere()
        {
            return new StudentException($"The student is already enrolled in this course");
        }

        public static StudentException InvalidMegafaclty()
        {
            return new StudentException($"A student cannot be enrolled in courses of their own stream");
        }
    }
}
