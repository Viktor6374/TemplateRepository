using System.Collections.Generic;
using Isu.Entities;
using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities
{
    public class AdvancedStudent : Student
    {
        private List<CombinedGroupTrainingAreas> combinedGroupTrainingAreas = new List<CombinedGroupTrainingAreas>();
        public AdvancedStudent(string name, AdvancedGroup group, int id)
            : base(name, group, id) => StudentGroup = group;
        public new AdvancedGroup StudentGroup { get; private set; }
        public IReadOnlyList<CombinedGroupTrainingAreas> CombinedGroupTrainingAreas => combinedGroupTrainingAreas.AsReadOnly();
        public void AddCombinedGroupTrainingAreas(CombinedGroupTrainingAreas combinedGroupTrainingAreas_)
        {
            if (combinedGroupTrainingAreas.Count == 2)
            {
                throw StudentException.OverflowCombinedGroupTrainingAreas();
            }

            if (combinedGroupTrainingAreas.Count == 1 && combinedGroupTrainingAreas_ == combinedGroupTrainingAreas[0])
            {
                throw StudentException.CourseIsAlreadyThere();
            }

            combinedGroupTrainingAreas.Add(combinedGroupTrainingAreas_ ?? throw InvalidArgumentException.NullArgument());
        }

        public void ChangeGroup(AdvancedGroup advancedGroup)
        {
            base.ChangeGroup(advancedGroup);
        }

        public void RemoveCombinedGroupTrainingAreas(CombinedGroupTrainingAreas combinedGroupTrainingAreas_)
        {
            combinedGroupTrainingAreas.Remove(combinedGroupTrainingAreas_);
        }
    }
}
