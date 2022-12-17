using Backups.Interfaces;

namespace Backups.Entities
{
    public class SplitStorage : IStorage
    {
        private List<IBackupObject> backupObjects = new List<IBackupObject>();
        private List<string> namesOfFile;
        public SplitStorage(List<IBackupObject> backupObjects_, List<string> namesOfFile_)
        {
            backupObjects.AddRange(backupObjects_ ?? throw new ArgumentNullException());
            namesOfFile = namesOfFile_ ?? throw new ArgumentNullException();
        }

        public IReadOnlyList<IBackupObject> BackupObjects() => backupObjects.AsReadOnly();
        public IReadOnlyList<string> NamesOfFiles() => namesOfFile.AsReadOnly();
    }
}
