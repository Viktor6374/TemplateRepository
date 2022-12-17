using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backups.Extra.Interfaces
{
    public interface IRestoreAlgorithm
    {
        public void Restore(IAdvancedRepository targetRepository, IAdvancedRepository sourceRepository);
    }
}
