using Isu.Exceptions;

namespace Isu.Models
{
    public class CourseNumber
    {
        private readonly int maxCourse = 4;
        private readonly int minCourse = 1;
        public CourseNumber(int course)
        {
            if (course > maxCourse && course < minCourse)
            {
                throw new InvalidCourseException();
            }

            Course = course;
        }

        public int Course { get; }
    }
}