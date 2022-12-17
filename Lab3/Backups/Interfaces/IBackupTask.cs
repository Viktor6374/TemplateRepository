namespace Backups.Interfaces
{
    public interface IBackupTask
    {
        public IReadOnlyList<IBackupObject> BackupObjects { get; }
        public IRepository Repository { get; }
        public string Name { get; }
        public void AddBackupObject(IBackupObject backupObject);
        public void RemoveBackupObject(IBackupObject backupObject);
        public void AddRestorePoint(IRestorePoint restorePoint);
        public void RemoveRestorePoint(IRestorePoint restorePoint);
    }
}
