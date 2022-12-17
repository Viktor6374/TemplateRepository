namespace Backups.Interfaces
{
    public interface IStorage
    {
        public IReadOnlyList<IBackupObject> BackupObjects();
        public IReadOnlyList<string> NamesOfFiles();
    }
}
