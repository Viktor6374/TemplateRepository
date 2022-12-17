using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities
{
    public class Lesson
    {
        private readonly int minNumberLesson = 1;
        private readonly int maxNumberLesson = 10;
        public Lesson(int numberLesson, string nameTeacher, int classroom, IStudyGroup studyGroup)
        {
            if (studyGroup == null || string.IsNullOrEmpty(nameTeacher))
            {
                throw InvalidArgumentException.NullArgument();
            }

            if (numberLesson < minNumberLesson || numberLesson > maxNumberLesson)
            {
                throw InvalidArgumentException.IncorrectValue();
            }

            if (classroom < 1101 || classroom > 9999)
            {
                throw InvalidArgumentException.IncorrectValue();
            }

            NumberLesson = numberLesson;
            NameTeacher = nameTeacher;
            Classroom = classroom;
            StudyGroup = studyGroup;
        }

        public int NumberLesson { get; }
        public string NameTeacher { get; }
        public int Classroom { get; }
        public IStudyGroup StudyGroup { get; }
    }
}
