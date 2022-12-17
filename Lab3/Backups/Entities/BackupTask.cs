using Backups.Interfaces;

namespace Backups.Entities
{
    public class BackupTask : IBackupTask
    {
        private List<IBackupObject> _backupObjects = new List<IBackupObject>();
        public BackupTask(IRepository repository, string name, IArchiver archiver, IStorageAlgorithm storageAlgorithm)
        {
            if (string.IsNullOrEmpty(name) || repository == null)
            {
                throw new ArgumentNullException();
            }

            Name = name;
            Repository = repository.StepForward(name);
            Backup = new Backup(storageAlgorithm ?? throw new ArgumentNullException(), archiver ?? throw new ArgumentNullException());
        }

        public Backup Backup { get; }
        public IRepository Repository { get; }
        public string Name { get; }
        public IReadOnlyList<IRestorePoint> RestorePoints => Backup.RestorePoints;
        public IReadOnlyList<IBackupObject> BackupObjects => _backupObjects.AsReadOnly();

        public void AddBackupObject(IBackupObject backupObject)
        {
            _backupObjects.Add(backupObject ?? throw new ArgumentNullException());
        }

        public void RemoveBackupObject(IBackupObject backupObject)
        {
            _backupObjects.Remove(backupObject ?? throw new ArgumentNullException());
        }

        public RestorePoint CreateRestorePoint(string name)
        {
            IStorage storage = Backup.StorageAlgorithm.CreateStorage(Backup.Archiver, _backupObjects, Repository, name);
            return new RestorePoint(storage, name, Repository.StepForward(name));
        }

        public void AddRestorePoint(IRestorePoint restorePoint)
        {
            Backup.AddRestorePoint(restorePoint);
        }

        public void RemoveRestorePoint(IRestorePoint restorePoint)
        {
            Backup.RemoveRestorePoint(restorePoint);
        }
    }
}
