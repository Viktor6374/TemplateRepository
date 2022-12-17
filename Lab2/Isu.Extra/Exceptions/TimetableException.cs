using System;

namespace Isu.Extra.Exceptions
{
    public class TimetableException : Exception
    {
        public TimetableException(string massage)
            : base(massage) { }
        public static TimetableException RepeatWeekDay()
        {
            return new TimetableException($"Such a day of the week already exists!");
        }

        public static TimetableException TimetableNotComplite()
        {
            return new TimetableException($"Timetable should contain 6 different days");
        }

        public static TimetableException IntersectionTimetable()
        {
            return new TimetableException($"Timetable overlap!");
        }
    }
}
