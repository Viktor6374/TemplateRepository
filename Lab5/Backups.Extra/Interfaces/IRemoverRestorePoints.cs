using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Extra.Interfaces
{
    public interface IRemoverRestorePoints
    {
        public void Remove(List<IRestorePoint> restorePoints, Backup backup);
    }
}
