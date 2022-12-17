using Isu.Exceptions;

namespace Isu.Entities
{
    public class Student
    {
        public Student(string name, Group group, int id)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidStudentName();
            if (group == null)
                throw new NullArgumentException();
            StudentName = name;
            StudentGroup = group;
            group.AddStudent(this);
            ID = id;
        }

        public string StudentName { get; }
        public int ID { get; }
        public Group StudentGroup { get; private set; }
        public void ChangeGroup(Group newGroup)
        {
            if (newGroup == null)
                throw new NullArgumentException();
            newGroup.AddStudent(this);
            StudentGroup.DeleteStudent(this);
            StudentGroup = newGroup;
        }
    }
}