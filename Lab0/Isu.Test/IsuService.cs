using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test
{
    public class IsuService
    {
        [Fact]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            var itmo = new IsuServices();
            var groupName = new GroupName("M32031");
            Group testGroup = itmo.AddGroup(groupName);
            Student testStudent = itmo.AddStudent(testGroup, "Ivanov Petr");
            Assert.Equal(testStudent.StudentGroup, testGroup);
            Assert.Equal(testStudent, testGroup.GetStudents()[0]);
        }

        [Fact]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            var itmo = new IsuServices();
            var groupName = new GroupName("M32031");
            Group testGroup = itmo.AddGroup(groupName);
            for (int i = 0; i < 20; i++)
            {
                itmo.AddStudent(testGroup, "Ivanov Petr");
            }

            Assert.Throws<CrowededGroupExeption>(() => itmo.AddStudent(testGroup, "Ivanov Petr"));
        }

        [Fact]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Throws<InvalidGroupNameException>(() => new GroupName("MMM32031"));
        }

        [Fact]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            var itmo = new IsuServices();
            var oldGroupName = new GroupName("M32031");
            var newGroupName = new GroupName("M32001");
            Group oldGroup = itmo.AddGroup(oldGroupName);
            Group newGroup = itmo.AddGroup(newGroupName);
            Student testStudent = itmo.AddStudent(oldGroup, "Ivanov Petr");
            testStudent.ChangeGroup(newGroup);
            Assert.Equal(testStudent.StudentGroup, newGroup);
            Assert.Equal(testStudent, newGroup.GetStudents()[0]);
            Assert.True(oldGroup.GetStudents().Count == 0);
        }
    }
}