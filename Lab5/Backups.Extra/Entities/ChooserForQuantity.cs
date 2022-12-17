using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Entities
{
    public class ChooserForQuantity : IChooserRestorePoint
    {
        public ChooserForQuantity(Backup backup, int limit, IRemoverRestorePoints remover)
        {
            Backup = backup ?? throw new ArgumentNullException();
            if (limit < 1)
            {
                throw new ArgumentException("Limit can't be <1");
            }

            Limit = limit;
            Remover = remover ?? throw new ArgumentNullException();
        }

        public IRemoverRestorePoints Remover { get; }
        public int Limit { get; }
        public Backup Backup { get; }

        public void CleanRestorePoints()
        {
            List<IRestorePoint> restorePointForRemoving = Backup.RestorePoints.OrderBy(x => x.DateTimeOfCreation).ToList().GetRange(0, Backup.RestorePoints.Count - Limit);
            Remover.Remove(restorePointForRemoving, Backup);
        }
    }
}
