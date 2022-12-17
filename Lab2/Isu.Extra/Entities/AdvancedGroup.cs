using System.Linq;
using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Models;

namespace Isu.Extra.Entities
{
    public class AdvancedGroup : Group
    {
        public AdvancedGroup(GroupName groupName)
            : base(groupName)
        {
            Timetable = Timetable.Builder.EmptyTimetable();
        }

        public AdvancedGroup(Timetable timetable, GroupName groupName)
            : base(groupName)
        {
            if (!timetable.TimetableDays.SelectMany(x => x.Lessons).All(x => x.StudyGroup == this))
            {
                throw StudyGroupException.InvalidTimetable();
            }

            Timetable = timetable ?? throw InvalidArgumentException.NullArgument();
        }

        public Timetable Timetable { get; protected set; }

        public void ChangeTimetable(Timetable timetable)
        {
            Timetable = timetable ?? throw InvalidArgumentException.NullArgument();
        }
    }
}
