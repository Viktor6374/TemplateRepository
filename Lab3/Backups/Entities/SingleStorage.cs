using Backups.Interfaces;

namespace Backups.Entities
{
    public class SingleStorage : IStorage
    {
        private List<IBackupObject> backupObjects;
        public SingleStorage(List<IBackupObject> backupObjects_, string nameOfFile_)
        {
            if (string.IsNullOrEmpty(nameOfFile_))
            {
                throw new ArgumentNullException();
            }

            NameOfFile = nameOfFile_;
            backupObjects = backupObjects_ ?? throw new ArgumentNullException();
        }

        public string NameOfFile { get; }
        public IReadOnlyList<IBackupObject> BackupObjects() => backupObjects.AsReadOnly();
        public IReadOnlyList<string> NamesOfFiles() => new List<string> { NameOfFile }.AsReadOnly();
    }
}
