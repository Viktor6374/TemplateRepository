using Backups.Algorithms;
using Backups.Entities;
using Backups.Interfaces;
using Xunit;
using Zio;

namespace Backups.Test
{
    public class BackupTaskTest
    {
        [Fact]
        public void AddSplitStorage()
        {
            var repository = new MemoryRepository(@"/tmp");
            var backupTask = new BackupTask(repository, "BackupTask1", new ZipArchiver(), new SplitStorageAlgorithm());
            using (Stream memoryStream = repository.WriteFile(@"File1.txt"))
            {
                using (var writer = new StreamWriter(memoryStream))
                {
                    writer.Write("File1");
                }
            }

            using (Stream memoryStream = repository.WriteFile(@"File2.txt"))
            {
                using (var writer = new StreamWriter(memoryStream))
                {
                    writer.Write("File2");
                }
            }

            backupTask.AddBackupObject(new BackupObjectFile(repository, "File1.txt"));
            backupTask.AddBackupObject(new BackupObjectFile(repository, "File2.txt"));
            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint1"));
            backupTask.RemoveBackupObject(backupTask.BackupObjects[0]);
            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint2"));
            Assert.Equal(2, backupTask.RestorePoints.Count);
            Assert.Equal(3, backupTask.RestorePoints.Sum(x => x.Storage.BackupObjects().Count));
        }

        [Fact]
        public void CheckingFileCreation()
        {
            var repository = new MemoryRepository(@"/tmp");
            var backupTask = new BackupTask(repository, "BackupTask1", new ZipArchiver(), new SingleStorageAlgorithm());
            IRepository repository1 = repository.StepForward("directory");
            using (Stream memoryStream = repository1.WriteFile(@"File1.txt"))
            {
                using (var writer = new StreamWriter(memoryStream))
                {
                    writer.Write("File1");
                }
            }

            using (Stream memoryStream = repository.WriteFile(@"File2.txt"))
            {
                using (var writer = new StreamWriter(memoryStream))
                {
                    writer.Write("File2");
                }
            }

            backupTask.AddBackupObject(new BackupObjectDirectory(repository1));
            backupTask.AddBackupObject(new BackupObjectFile(repository, "File2.txt"));
            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint1"));
            Assert.True(repository.FileSystem.FileExists(UPath.Combine(new UPath("/tmp/BackupTask1/RestorePoint1"), new UPath(backupTask.RestorePoints[0].Storage.NamesOfFiles()[0])).ToString()));
        }
    }
}
