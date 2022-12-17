using System.Collections.Generic;
using System.Linq;
using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities
{
    public class CombinedGroupTrainingAreas
    {
        private List<Flow> flows = new List<Flow>();
        public CombinedGroupTrainingAreas(string name, MegaFaculty megaFaculty)
        {
            if (string.IsNullOrEmpty(name) || megaFaculty == null)
            {
                throw InvalidArgumentException.NullArgument();
            }

            Name = name;
            MegaFaculty = megaFaculty;
        }

        public MegaFaculty MegaFaculty { get; }
        public string Name { get; }
        public IReadOnlyList<Flow> Flows => flows.AsReadOnly();
        public Flow AddFlow(Flow flow)
        {
            flows.Add(flow);
            return flow;
        }

        public void AddStudent(AdvancedStudent student, Flow flow)
        {
            if (student == null || flow == null)
            {
                throw InvalidArgumentException.NullArgument();
            }

            if (flows.Where(x => x == flow).FirstOrDefault() == null)
            {
                throw StudyGroupException.InvalidFlow();
            }

            if (flow.Timetable.CheckIntersectionTimetable(student.StudentGroup.Timetable))
            {
                throw TimetableException.IntersectionTimetable();
            }

            if (MegaFaculty.LetterIdentifier == student.StudentGroup.GroupName.Name[0])
            {
                throw StudentException.InvalidMegafaclty();
            }

            student.AddCombinedGroupTrainingAreas(this);
            flow.AddStudent(student);
        }

        public void RemoveStudent(AdvancedStudent student, Flow flow)
        {
            flow.DeleteStudent(student);
            student.RemoveCombinedGroupTrainingAreas(this);
        }
    }
}
