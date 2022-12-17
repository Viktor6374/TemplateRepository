using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backups.Entities;
using Backups.Extra.Enams;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Entities
{
    public class HybridChooser : IChooserRestorePoint
    {
        public HybridChooser(Backup backup, int limitQuantity, DateTime limitDateTime, RemoveMethod removeMethod, IRemoverRestorePoints remover)
        {
            if (limitQuantity < 1)
            {
                throw new ArgumentOutOfRangeException("limitQuantity can't be <1");
            }

            LimitQuantity = limitQuantity;
            LimitDateTime = limitDateTime;
            Backup = backup ?? throw new ArgumentNullException();
            RemoveMethod = removeMethod;
            Remover = remover ?? throw new ArgumentNullException();
        }

        public Backup Backup { get; }
        public RemoveMethod RemoveMethod { get; }
        public int LimitQuantity { get; }
        public DateTime LimitDateTime { get; }
        public IRemoverRestorePoints Remover { get; }
        public void CleanRestorePoints()
        {
            var removingForDateTime = Backup.RestorePoints.Where(x => x.DateTimeOfCreation < LimitDateTime).ToList();
            List<IRestorePoint> removingForQuantity = Backup.RestorePoints.OrderBy(x => x.DateTimeOfCreation).ToList().GetRange(0, Backup.RestorePoints.Count - LimitQuantity);
            List<IRestorePoint> resultForRemoving;
            if (RemoveMethod == RemoveMethod.OneSign)
            {
                resultForRemoving = removingForDateTime.Union(removingForQuantity).ToList();
            }
            else
            {
                resultForRemoving = removingForDateTime.Intersect(removingForQuantity).ToList();
            }

            if (resultForRemoving.Count == Backup.RestorePoints.Count)
            {
                resultForRemoving.Remove(resultForRemoving.OrderBy(x => x.DateTimeOfCreation).ToList().Last());
            }

            Remover.Remove(resultForRemoving, Backup);
        }
    }
}
