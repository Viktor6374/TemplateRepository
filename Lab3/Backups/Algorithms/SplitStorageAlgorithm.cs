using Backups.Entities;
using Backups.Interfaces;
using Zio;
namespace Backups.Algorithms
{
    public class SplitStorageAlgorithm : IStorageAlgorithm
    {
        public IStorage CreateStorage(IArchiver archiver, List<IBackupObject> backupObjects, IRepository repository, string name)
        {
            var namesOfFile = new List<string>();
            repository.CreateDirectory(name);
            IRepository repository1 = repository.StepForward(name);
            foreach (IBackupObject backupObject in backupObjects)
            {
                if (backupObject is BackupObjectFile)
                {
                    var objectFile = (BackupObjectFile)backupObject;
                    namesOfFile.Add(archiver.Archive(new List<IBackupObject> { backupObject }, repository1, objectFile.Name));
                }
                else if (backupObject is BackupObjectDirectory)
                {
                    var objectDirectory = (BackupObjectDirectory)backupObject;
                    namesOfFile.Add(archiver.Archive(new List<IBackupObject> { backupObject }, repository1, new UPath(objectDirectory.Repository.Path()).GetName()));
                }
            }

            return new SplitStorage(backupObjects, namesOfFile);
        }
    }
}
