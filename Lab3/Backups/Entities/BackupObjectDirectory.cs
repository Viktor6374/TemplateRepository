using Backups.Interfaces;

namespace Backups.Entities
{
    public class BackupObjectDirectory : IBackupObject
    {
        public BackupObjectDirectory(IRepository repository)
        {
            Repository = repository ?? throw new ArgumentNullException();
        }

        public IRepository Repository { get; }
    }
}
