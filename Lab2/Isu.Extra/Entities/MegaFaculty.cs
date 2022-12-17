using System.Collections.Generic;
using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities
{
    public class MegaFaculty
    {
        private List<CombinedGroupTrainingAreas> combinedGroupTrainingAreas = new List<CombinedGroupTrainingAreas>();
        public MegaFaculty(string name, char letterIdentifier)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw InvalidArgumentException.NullArgument();
            }

            Name = name;
            LetterIdentifier = letterIdentifier;
        }

        public string Name { get; }
        public char LetterIdentifier { get; }
        public IReadOnlyList<CombinedGroupTrainingAreas> CombinedGroupTrainingAreas => combinedGroupTrainingAreas.AsReadOnly();
        public CombinedGroupTrainingAreas CreateCombinedGroupTrainingAreas(string name)
        {
            var combinedGroupTrainingAreas_ = new CombinedGroupTrainingAreas(name, this);
            combinedGroupTrainingAreas.Add(combinedGroupTrainingAreas_);
            return combinedGroupTrainingAreas_;
        }
    }
}
