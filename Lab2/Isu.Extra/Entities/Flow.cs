using System.Collections.Generic;
using System.Linq;
using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities
{
    public class Flow : IStudyGroup
    {
        private static readonly int MaxStudents = 50;
        private List<AdvancedStudent> students = new List<AdvancedStudent>();
        public Flow(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw InvalidArgumentException.NullArgument();
            }

            Name = name;
            Timetable = Timetable.Builder.EmptyTimetable();
        }

        public Flow(string name, Timetable timetable)
        {
            if (!timetable.TimetableDays.SelectMany(x => x.Lessons).All(x => x.StudyGroup == this))
            {
                throw StudyGroupException.InvalidTimetable();
            }

            if (string.IsNullOrEmpty(name))
            {
                throw InvalidArgumentException.NullArgument();
            }

            Timetable = timetable ?? throw InvalidArgumentException.NullArgument();
            Name = name;
        }

        public string Name { get; }
        public Timetable Timetable { get; private set; }
        public IReadOnlyList<AdvancedStudent> Students => students.AsReadOnly();

        public void AddStudent(AdvancedStudent student)
        {
            if (students.Count == MaxStudents)
            {
                throw StudyGroupException.CrowdedGroup();
            }

            if (students.Contains(student))
            {
                throw StudentException.TheStudentIsAlreadyInTheGroupException();
            }

            students.Add(student);
        }

        public void DeleteStudent(AdvancedStudent student)
        {
            if (student == null)
            {
                throw InvalidArgumentException.NullArgument();
            }

            if (!students.Remove(student))
                throw StudentException.TheStudentIsNotInThisGroupException();
        }

        public void ChangeTimetable(Timetable timetable)
        {
            Timetable = timetable ?? throw InvalidArgumentException.NullArgument();
        }
    }
}
