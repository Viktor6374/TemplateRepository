using System.IO.Compression;
using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Algorithms
{
    public class ZipArchiver : IArchiver
    {
        public string Archive(List<IBackupObject> backupObjects, IRepository repository, string nameFile)
        {
            using (Stream zipToOpen = repository.WriteFile(@$"{nameFile}.zip"))
            {
                using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    WriteBackupObject(archive, backupObjects);
                }
            }

            return @$"{nameFile}.zip";
        }

        private void WriteBackupObject(ZipArchive archive, List<IBackupObject> backupObjects)
        {
            foreach (IBackupObject backupObject in backupObjects)
            {
                if (backupObject is BackupObjectFile)
                {
                    var backupObject1 = (BackupObjectFile)backupObject;
                    WriteEntry(archive, backupObject1.Name, backupObject);
                }
                else if (backupObject is BackupObjectDirectory)
                {
                    foreach (string file in backupObject.Repository.GetAllFile())
                    {
                        WriteEntry(archive, file, backupObject);
                    }
                }
                else
                {
                    throw new Exception("Invalid type IBackupObject");
                }
            }
        }

        private void WriteEntry(ZipArchive archive, string nameFile, IBackupObject backupObject)
        {
            ZipArchiveEntry readmeEntry = archive.CreateEntry(nameFile);
            using (Stream writer = readmeEntry.Open())
            {
                using (Stream stream = backupObject.Repository.ReadFile(nameFile))
                {
                    stream.CopyTo(writer);
                }
            }
        }
    }
}
