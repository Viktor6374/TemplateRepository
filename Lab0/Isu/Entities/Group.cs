using System.Collections.Generic;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities
{
    public class Group
    {
        protected static readonly int MaxStudents = 20;
        private List<Student> students = new List<Student>();
        public Group(GroupName groupName)
        {
            if (groupName == null)
            {
                throw new InvalidGroupNameException();
            }

            GroupName = groupName;
        }

        public Group(string stringName)
        {
            var groupName = new GroupName(stringName);
            GroupName = groupName;
        }

        public GroupName GroupName { get; }
        public IReadOnlyList<Student> GetStudents()
        {
            return students.AsReadOnly();
        }

        public void AddStudent(Student student)
        {
            if (students.Count == MaxStudents)
            {
                throw new CrowededGroupExeption();
            }

            if (students.Contains(student))
            {
                throw new TheStudentIsAlreadyInTheGroupException();
            }

            students.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            if (student == null)
            {
                throw new NullArgumentException();
            }

            if (!students.Remove(student))
                throw new TheStudentIsNotInThisGroupException();
        }
    }
}