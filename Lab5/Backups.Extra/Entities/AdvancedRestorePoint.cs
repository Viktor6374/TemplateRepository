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
    public class AdvancedRestorePoint : RestorePoint
    {
        public AdvancedRestorePoint(IStorage storage, string name, IAdvancedRepository repository)
            : base(storage, name, repository) { }
        public new IRepository Repository => (IAdvancedRepository)Repository;
    }
}
