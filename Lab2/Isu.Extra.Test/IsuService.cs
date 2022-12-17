using Isu.Entities;
using Isu.Exceptions;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Models;
using Xunit;

namespace Isu.Extra.Test
{
    public class IsuService
    {
        [Fact]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            var itmo = new Service();
            var groupName = new GroupName("M32031");
            Group testGroup = itmo.AddGroup(groupName);
            Student testStudent = itmo.AddStudent(testGroup, "Ivanov Petr");
            Assert.Equal(testStudent.StudentGroup, testGroup);
            Assert.Equal(testStudent, testGroup.GetStudents()[0]);
        }

        [Fact]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            var itmo = new Service();
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
            var itmo = new Service();
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

        [Fact]
        public void RecordAndRemoveCombinedGroupTrainingAreas()
        {
            var itmo = new Service();
            var groupName = new GroupName("K32031");
            AdvancedGroup testGroup = itmo.AddGroup(groupName);
            AdvancedStudent testStudent = itmo.AddStudent(testGroup, "Ivanov Petr");
            MegaFaculty megaFaculty = itmo.AddMegaFaculty("fitip", 'M');
            CombinedGroupTrainingAreas combinedGroupTrainingAreas = itmo.AddCombinedGroupTrainingAreas(megaFaculty, "history");
            Flow flow = combinedGroupTrainingAreas.AddFlow(new Flow("K3205", Timetable.Builder.EmptyTimetable()));
            Assert.Equal(combinedGroupTrainingAreas, itmo.MegaFaculties[0].CombinedGroupTrainingAreas[0]);

            itmo.RecordStudent(testStudent, flow, combinedGroupTrainingAreas);
            Assert.Equal(flow, itmo.MegaFaculties[0].CombinedGroupTrainingAreas[0].Flows[0]);

            itmo.RemoveRecordStudent(testStudent, flow, combinedGroupTrainingAreas);
            Assert.Equal(0, itmo.MegaFaculties[0].CombinedGroupTrainingAreas[0].Flows[0].Students.Count);
        }

        [Fact]
        public void GettingStreamsAtTheRate()
        {
            var itmo = new Service();
            MegaFaculty megaFaculty = itmo.AddMegaFaculty("fitip", 'M');
            CombinedGroupTrainingAreas combinedGroupTrainingAreas = itmo.AddCombinedGroupTrainingAreas(megaFaculty, "history");
            Flow flow1 = combinedGroupTrainingAreas.AddFlow(new Flow("K3205", Timetable.Builder.EmptyTimetable()));
            Flow flow2 = combinedGroupTrainingAreas.AddFlow(new Flow("K3206", Timetable.Builder.EmptyTimetable()));
            Flow flow3 = combinedGroupTrainingAreas.AddFlow(new Flow("K3207", Timetable.Builder.EmptyTimetable()));

            Assert.Equal(flow1, itmo.GetFlowsOfCourse(combinedGroupTrainingAreas)[0]);
            Assert.Equal(flow2, itmo.GetFlowsOfCourse(combinedGroupTrainingAreas)[1]);
            Assert.Equal(flow3, itmo.GetFlowsOfCourse(combinedGroupTrainingAreas)[2]);
        }

        [Fact]
        public void GettingStudentsOnFlow()
        {
            var itmo = new Service();
            MegaFaculty megaFaculty = itmo.AddMegaFaculty("fitip", 'M');
            CombinedGroupTrainingAreas combinedGroupTrainingAreas = itmo.AddCombinedGroupTrainingAreas(megaFaculty, "history");
            Flow flow = combinedGroupTrainingAreas.AddFlow(new Flow("K3205", Timetable.Builder.EmptyTimetable()));
            var groupName = new GroupName("K32031");
            AdvancedGroup testGroup = itmo.AddGroup(groupName);
            AdvancedStudent testStudent1 = itmo.AddStudent(testGroup, "Ivanov Petr");
            AdvancedStudent testStudent2 = itmo.AddStudent(testGroup, "Ivanov Ivan");
            AdvancedStudent testStudent3 = itmo.AddStudent(testGroup, "Ivanov Denis");

            itmo.RecordStudent(testStudent1, flow, combinedGroupTrainingAreas);
            itmo.RecordStudent(testStudent2, flow, combinedGroupTrainingAreas);
            itmo.RecordStudent(testStudent3, flow, combinedGroupTrainingAreas);

            Assert.Equal(testStudent1, itmo.GetStudentsInFlow(flow)[0]);
            Assert.Equal(testStudent2, itmo.GetStudentsInFlow(flow)[1]);
            Assert.Equal(testStudent3, itmo.GetStudentsInFlow(flow)[2]);
        }

        [Fact]
        public void GettingListOfStudentsWhoHaveNotEnrolledInCoursesByGroup()
        {
            var itmo = new Service();
            MegaFaculty megaFaculty = itmo.AddMegaFaculty("fitip", 'M');
            CombinedGroupTrainingAreas combinedGroupTrainingAreas = itmo.AddCombinedGroupTrainingAreas(megaFaculty, "history");
            Flow flow = combinedGroupTrainingAreas.AddFlow(new Flow("K3205", Timetable.Builder.EmptyTimetable()));
            var groupName = new GroupName("K32031");
            AdvancedGroup testGroup = itmo.AddGroup(groupName);
            AdvancedStudent testStudent1 = itmo.AddStudent(testGroup, "Ivanov Petr");
            AdvancedStudent testStudent2 = itmo.AddStudent(testGroup, "Ivanov Ivan");
            AdvancedStudent testStudent3 = itmo.AddStudent(testGroup, "Ivanov Denis");

            Assert.Equal(testStudent1, itmo.GetStudentsWithoutCourse(testGroup)[0]);
            Assert.Equal(testStudent2, itmo.GetStudentsWithoutCourse(testGroup)[1]);
            Assert.Equal(testStudent3, itmo.GetStudentsWithoutCourse(testGroup)[2]);

            itmo.RecordStudent(testStudent1, flow, combinedGroupTrainingAreas);
            itmo.RecordStudent(testStudent2, flow, combinedGroupTrainingAreas);

            Assert.Equal(testStudent3, itmo.GetStudentsWithoutCourse(testGroup)[0]);
            Assert.Equal(1, itmo.GetStudentsWithoutCourse(testGroup).Count);
        }

        [Fact]
        public void AddingStudentToYourFacultyFlow()
        {
            var itmo = new Service();
            MegaFaculty megaFaculty = itmo.AddMegaFaculty("fitip", 'M');
            CombinedGroupTrainingAreas combinedGroupTrainingAreas = itmo.AddCombinedGroupTrainingAreas(megaFaculty, "history");
            Flow flow = combinedGroupTrainingAreas.AddFlow(new Flow("K3205", Timetable.Builder.EmptyTimetable()));
            var groupName = new GroupName("M32031");
            AdvancedGroup testGroup = itmo.AddGroup(groupName);
            AdvancedStudent testStudent1 = itmo.AddStudent(testGroup, "Ivanov Petr");
            Assert.Throws<StudentException>(() => itmo.RecordStudent(testStudent1, flow, combinedGroupTrainingAreas));
        }
    }
}
