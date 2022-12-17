using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backups.Entities;
using Backups.Extra.Interfaces;

namespace Backups.Extra.Entities
{
    public class ChooserForDateTime : IChooserRestorePoint
    {
        public ChooserForDateTime(Backup backup, DateTime limit, IRemoverRestorePoints remover)
        {
            Backup = backup ?? throw new ArgumentNullException();
            Limit = limit;
            Remover = remover ?? throw new ArgumentNullException();
        }

        public Backup Backup { get; }
        public DateTime Limit { get; }
        public IRemoverRestorePoints Remover { get; }
        public void CleanRestorePoints()
        {
            var restorePointForRemoving = Backup.RestorePoints.Where(x => x.DateTimeOfCreation < Limit).ToList();
            if (restorePointForRemoving.Count == Backup.RestorePoints.Count)
            {
                restorePointForRemoving.Remove(restorePointForRemoving.OrderBy(x => x.DateTimeOfCreation).ToList().Last());
            }

            Remover.Remove(restorePointForRemoving, Backup);
        }
    }
}
