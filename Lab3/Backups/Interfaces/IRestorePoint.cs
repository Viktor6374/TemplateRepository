namespace Backups.Interfaces
{
    public interface IRestorePoint
    {
        public IStorage Storage { get; }
        public DateTime DateTimeOfCreation { get; }
        public string Name { get; }
        public IRepository Repository { get; }
    }
}
