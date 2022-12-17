using Backups.Interfaces;

namespace Backups.Extra.Interfaces
{
    public interface IRestorer
    {
        public void Restore(IRestorePoint restorePoint, IRestoreAlgorithm restoreAlgorithm, IAdvancedRepository? repository = null);
    }
}
