using Backups.Interfaces;

namespace Backups.Entities
{
    public class Backup
    {
        private List<IRestorePoint> _restorePoints = new List<IRestorePoint>();
        public Backup(IStorageAlgorithm storageAlgorithm, IArchiver archiver)
        {
            Archiver = archiver ?? throw new ArgumentNullException();
            StorageAlgorithm = storageAlgorithm ?? throw new ArgumentNullException();
        }

        public IArchiver Archiver { get; }
        public IStorageAlgorithm StorageAlgorithm { get; }
        public IReadOnlyList<IRestorePoint> RestorePoints => _restorePoints.AsReadOnly();
        public void AddRestorePoint(IRestorePoint restorePoint)
        {
            _restorePoints.Add(restorePoint ?? throw new ArgumentNullException());
        }

        public void RemoveRestorePoint(IRestorePoint restorePoint)
        {
            _restorePoints.Remove(restorePoint ?? throw new ArgumentNullException());
        }
    }
}
