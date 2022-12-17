using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Algorithms
{
    public class SingleStorageAlgorithm : IStorageAlgorithm
    {
        public IStorage CreateStorage(IArchiver archiver, List<IBackupObject> backupObjects, IRepository repository, string name)
        {
            repository.CreateDirectory(name);
            IRepository repository1 = repository.StepForward(name);
            string nameFile = archiver.Archive(backupObjects, repository1, "SingleArchive");
            return new SingleStorage(backupObjects, nameFile);
        }
    }
}
