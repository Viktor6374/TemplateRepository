using Backups.Entities;
using Backups.Extra.Interfaces;

namespace Backups.Extra.Entities
{
    public class AdvancedBackupObjectFile : BackupObjectFile, IAdvancedBackupObject
    {
        public AdvancedBackupObjectFile(IAdvancedRepository repository, string name)
            : base(repository, name) { }
        public IAdvancedRepository AdvancedRepository => (IAdvancedRepository)Repository;
    }
}
