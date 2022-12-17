namespace Backups.Interfaces
{
    public interface IStorageAlgorithm
    {
        public IStorage CreateStorage(IArchiver archiver, List<IBackupObject> backupObjects, IRepository repository, string name);
    }
}
