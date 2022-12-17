using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services
{
    public class IsuServices : IIsuService
    {
        public List<Group> University { get; } = new List<Group>();
        protected IdGenerator IdGenerator { get; } = new IdGenerator();
        public Group AddGroup(GroupName name)
        {
            var group = new Group(name);
            if (University.Contains(group))
            {
                throw new TheGroupAlreadyExists();
            }

            University.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            int id = IdGenerator.Generate();
            var student = new Student(name, group, id);
            return student;
        }

        public Student GetStudent(int id)
        {
            Student? result = FindStudent(id);
            if (result == null)
                throw new TheStudentDoesNotExistException();
            return result;
        }

        public Student? FindStudent(int id)
        {
            return (from group_ in University
                    from student_ in group_.GetStudents()
                    where student_.ID == id
                    select student_).SingleOrDefault();
        }

        public List<Student> FindStudents(GroupName groupName)
        {
            return (from group_ in University
                    where group_.GroupName == groupName
                    select group_.GetStudents().ToList()).Single();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return (from group_ in University
                    where group_.GroupName.CourseNumber == courseNumber
                    select group_.GetStudents().ToList()).SelectMany(x => x).ToList();
        }

        public Group? FindGroup(GroupName groupName)
        {
            return (from group_ in University
                    where group_.GroupName == groupName
                    select group_).SingleOrDefault();
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return (from group_ in University
                   where group_.GroupName.CourseNumber == courseNumber
                   select group_).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (student == null || newGroup == null)
                throw new NullArgumentException();
            student.ChangeGroup(newGroup);
        }
    }
}
