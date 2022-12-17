using Backups.Entities;

namespace Backups.Extra.Interfaces
{
    public interface IChooserRestorePoint
    {
        public Backup Backup { get; }
        public void CleanRestorePoints();
    }
}
