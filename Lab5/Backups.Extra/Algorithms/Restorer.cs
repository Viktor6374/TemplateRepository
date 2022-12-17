using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backups.Extra.Interfaces;
using Backups.Interfaces;
using Zio;

namespace Backups.Extra.Algorithms
{
    public class Restorer : IRestorer
    {
        public void Restore(IRestorePoint restorePoint, IRestoreAlgorithm restoreAlgorithm, IAdvancedRepository? repository = null)
        {
            foreach (IAdvancedBackupObject backupObject in restorePoint.Storage.BackupObjects())
            {
                IAdvancedRepository targetRepository = repository ?? (IAdvancedRepository)backupObject.Repository;

                restoreAlgorithm.Restore(targetRepository, (IAdvancedRepository)restorePoint.Repository);
            }
        }
    }
}
