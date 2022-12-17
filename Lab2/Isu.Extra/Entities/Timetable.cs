using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities
{
    public class Timetable
    {
        private List<TimetableDay> timetableDays = new List<TimetableDay>();
        private Timetable(List<TimetableDay> timetableDays_)
        {
            timetableDays = timetableDays_;
        }

        public static BuilderTimetable Builder => new BuilderTimetable();
        public IReadOnlyList<TimetableDay> TimetableDays => timetableDays.AsReadOnly();
        public bool CheckIntersectionTimetable(Timetable timetable)
        {
            foreach (TimetableDay timetableDay in timetableDays)
            {
                if (timetable.TimetableDays.Any(x => x.CheckIntersectionTimetableDay(timetableDay)))
                {
                    return true;
                }
            }

            return false;
        }

        public class BuilderTimetable
        {
            public List<TimetableDay> TimetableDays { get; private set; } = new List<TimetableDay>();
            public void AddDay(TimetableDay timetableDay)
            {
                TimetableDay? nextDay = TimetableDays.Where(x => x.WeekDay > timetableDay.WeekDay).FirstOrDefault();
                if (TimetableDays.Where(x => x.WeekDay == timetableDay.WeekDay).SingleOrDefault() == null)
                {
                    if (nextDay == null)
                    {
                        TimetableDays.Add(timetableDay);
                    }
                    else
                    {
                        TimetableDays.Insert(TimetableDays.IndexOf(nextDay), timetableDay);
                    }
                }
                else
                {
                    throw TimetableException.RepeatWeekDay();
                }
            }

            public void ReplaceDay(TimetableDay timetableDay)
            {
                TimetableDay thisDay = TimetableDays.Where(x => x.WeekDay == timetableDay.WeekDay).Single();
                TimetableDays.Remove(thisDay);
                AddDay(timetableDay);
            }

            public void RemoveDay(TimetableDay timetableDay)
            {
                TimetableDays.Remove(timetableDay);
            }

            public Timetable EmptyTimetable()
            {
                foreach (int i in Enum.GetValues(typeof(WeekDay)))
                {
                    AddDay(TimetableDay.Builder.Build((WeekDay)i));
                }

                return Build();
            }

            public Timetable Build()
            {
                if (TimetableDays.Count != 7)
                {
                    throw TimetableException.TimetableNotComplite();
                }

                return new Timetable(TimetableDays);
            }
        }
    }
}
