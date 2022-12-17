using System.Collections.Generic;
using System.Linq;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;

namespace Isu.Extra.Models
{
    public class TimetableDay
    {
        private List<Lesson> lessons;
        private TimetableDay(List<Lesson> lessons, WeekDay weekDay)
        {
            this.lessons = lessons;
            WeekDay = weekDay;
        }

        public static BuilderTimetableDay Builder => new BuilderTimetableDay();

        public WeekDay WeekDay { get; }
        public IReadOnlyList<Lesson> Lessons => lessons.AsReadOnly();
        public bool CheckIntersectionTimetableDay(TimetableDay timetableDay)
        {
            foreach (Lesson lesson in lessons)
            {
                if (timetableDay.lessons.Any(x => x.NumberLesson == lesson.NumberLesson))
                {
                    return true;
                }
            }

            return false;
        }

        public class BuilderTimetableDay
        {
            private List<Lesson> lessons = new List<Lesson>();
            public IReadOnlyList<Lesson> Lessons => lessons.AsReadOnly();
            public void AddLesson(Lesson lesson)
            {
                if (lessons.Any(x => x.NumberLesson == lesson.NumberLesson))
                {
                    throw TimetableException.IntersectionTimetable();
                }

                lessons.Add(lesson ?? throw InvalidArgumentException.NullArgument());
            }

            public void RemoveLesson(Lesson lesson)
            {
                lessons.Remove(lesson);
            }

            public TimetableDay Build(WeekDay weekDay)
            {
                return new TimetableDay(lessons, weekDay);
            }
        }
    }
}
