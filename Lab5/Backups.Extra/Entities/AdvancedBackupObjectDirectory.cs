using Backups.Entities;
using Backups.Extra.Interfaces;

namespace Backups.Extra.Entities
{
    public class AdvancedBackupObjectDirectory : BackupObjectDirectory, IAdvancedBackupObject
    {
        public AdvancedBackupObjectDirectory(IAdvancedRepository repository)
            : base(repository) { }

        public IAdvancedRepository AdvancedRepository => (IAdvancedRepository)Repository;
    }
}
