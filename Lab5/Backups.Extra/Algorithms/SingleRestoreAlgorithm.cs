using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backups.Extra.Interfaces;

namespace Backups.Extra.Algorithms
{
    public class SingleRestoreAlgorithm : IRestoreAlgorithm
    {
        public void Restore(IAdvancedRepository targetRepository, IAdvancedRepository sourceRepository)
        {
            var zipArchive = new ZipArchive(sourceRepository.ReadFile(sourceRepository.GetAllFile()[0]));

            foreach (ZipArchiveEntry entry in zipArchive.Entries)
            {
                using (Stream source = entry.Open())
                {
                    using (Stream target = targetRepository.WriteFile(entry.FullName))
                    {
                        source.CopyTo(target);
                    }
                }
            }
        }
    }
}
