using Backups.Interfaces;

namespace Backups.Extra.Interfaces
{
    public interface IAdvancedBackupObject : IBackupObject
    {
        public IAdvancedRepository AdvancedRepository { get; }
    }
}
