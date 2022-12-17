using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Algorithms
{
    public class Remover : IRemoverRestorePoints
    {
        public void Remove(List<IRestorePoint> restorePoints, Backup backup)
        {
            foreach (IRestorePoint restorePoint in restorePoints)
            {
                var advancedRepository = (IAdvancedRepository)restorePoint.Repository;
                advancedRepository.RemoveDirectory(string.Empty);
                backup.RemoveRestorePoint(restorePoint);
            }
        }
    }
}
