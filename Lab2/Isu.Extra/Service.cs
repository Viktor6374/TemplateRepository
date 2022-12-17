using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Exceptions;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra
{
    public class Service : IsuServices
    {
        private List<MegaFaculty> megaFaculties = new List<MegaFaculty>();
        public IReadOnlyList<MegaFaculty> MegaFaculties => megaFaculties.AsReadOnly();
        public MegaFaculty AddMegaFaculty(string name, char letterIdentifier)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw InvalidArgumentException.NullArgument();
            }

            var megaFaculty = new MegaFaculty(name, letterIdentifier);
            megaFaculties.Add(megaFaculty);
            return megaFaculty;
        }

        public void RecordStudent(AdvancedStudent student, Flow flow, CombinedGroupTrainingAreas combinedGroupTrainingAreas)
        {
            combinedGroupTrainingAreas.AddStudent(student, flow);
        }

        public void RemoveRecordStudent(AdvancedStudent student, Flow flow, CombinedGroupTrainingAreas combinedGroupTrainingAreas)
        {
            combinedGroupTrainingAreas.RemoveStudent(student, flow);
        }

        public IReadOnlyList<Flow> GetFlowsOfCourse(CombinedGroupTrainingAreas combinedGroupTrainingAreas)
        {
            return combinedGroupTrainingAreas.Flows;
        }

        public IReadOnlyList<AdvancedStudent> GetStudentsInFlow(Flow flow)
        {
            return flow.Students;
        }

        public IReadOnlyList<Student> GetStudentsWithoutCourse(AdvancedGroup group)
        {
            return group.GetStudents().Where(y => MegaFaculties.SelectMany(x => x.CombinedGroupTrainingAreas).SelectMany(x => x.Flows).SelectMany(x => x.Students).All(x => x != y)).ToList().AsReadOnly();
        }

        public CombinedGroupTrainingAreas AddCombinedGroupTrainingAreas(MegaFaculty megaFaculty, string name)
        {
            return megaFaculty.CreateCombinedGroupTrainingAreas(name);
        }

        public new AdvancedGroup AddGroup(GroupName name)
        {
            var group = new AdvancedGroup(name);
            if (University.Contains(group))
            {
                throw new TheGroupAlreadyExists();
            }

            University.Add(group);
            return group;
        }

        public AdvancedStudent AddStudent(AdvancedGroup group, string name)
        {
            int id = IdGenerator.Generate();
            var student = new AdvancedStudent(name, group, id);
            return student;
        }
    }
}
