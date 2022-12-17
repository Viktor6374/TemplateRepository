using Backups.Interfaces;

namespace Backups.Entities
{
    public class RestorePoint : IRestorePoint
    {
        public RestorePoint(IStorage storage, string name, IRepository repository)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException();
            }

            Name = name;
            Storage = storage ?? throw new ArgumentNullException();
            Repository = repository ?? throw new ArgumentNullException();
        }

        public IStorage Storage { get; }
        public DateTime DateTimeOfCreation { get; } = DateTime.Now;
        public string Name { get; }
        public IRepository Repository { get; }
    }
}
