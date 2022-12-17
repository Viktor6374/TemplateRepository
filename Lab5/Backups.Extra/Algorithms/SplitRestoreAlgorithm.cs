using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backups.Extra.Interfaces;

namespace Backups.Extra.Algorithms
{
    public class SplitRestoreAlgorithm : IRestoreAlgorithm
    {
        public void Restore(IAdvancedRepository targetRepository, IAdvancedRepository sourceRepository)
        {
            foreach (string path in sourceRepository.GetAllFile())
            {
                var zipArchive = new ZipArchive(sourceRepository.ReadFile(path));
                using (Stream sourse = zipArchive.Entries[0].Open())
                {
                    using (Stream target = targetRepository.WriteFile(zipArchive.Entries[0].FullName))
                    {
                        sourse.CopyTo(target);
                    }
                }
            }
        }
    }
}
