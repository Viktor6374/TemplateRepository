using Backups.Algorithms;
using Backups.Extra.Algorithms;
using Backups.Extra.Enams;
using Backups.Extra.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;
using Xunit;

namespace Backups.Extra.Test
{
    public class BackupsExtraTest
    {
        [Fact]
        public void TestingLoggersWrite()
        {
            var repository = new AdvancedMemoryRepository(@"/tmp");
            var logger = new FileLogger(repository, "log.txt", OptionLogger.Default);
            var backupTask = new DecoratorBackupTask(repository, "BackupTask1", new ZipArchiver(), new SplitStorageAlgorithm(), logger);
            backupTask.AddChooserRestorePoint(new ChooserForQuantity(backupTask.Backup, 10, new Remover()));

            AddBackupFile(backupTask, repository, "File1.txt");

            string result;
            using (Stream file = repository.ReadFile(@"log.txt"))
            {
                using (var reader = new StreamReader(file))
                {
                    result = reader.ReadToEnd();
                }
            }

            Assert.Equal("Add BackupObject, type: file, path: /tmp, name: File1.txt\n", result);
        }

        [Fact]
        public void CleaningRestorePointsForQuantity()
        {
            DecoratorBackupTask backupTask = Initialize();

            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint1"));
            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint2"));
            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint3"));
            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint4"));

            backupTask.AddChooserRestorePoint(new ChooserForQuantity(backupTask.Backup, 2, new Remover()));

            backupTask.CleanRestorePoint();

            Assert.Equal(2, backupTask.RestorePoints.Count);
        }

        [Fact]
        public void CleaningRestorePointsForDateTime()
        {
            DecoratorBackupTask backupTask = Initialize();

            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint1"));
            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint2"));
            Thread.Sleep(5000);
            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint3"));
            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint4"));

            DateTime dateTime = backupTask.Backup.RestorePoints[3].DateTimeOfCreation;
            backupTask.AddChooserRestorePoint(new ChooserForDateTime(backupTask.Backup, dateTime.AddSeconds(-3), new Remover()));

            backupTask.CleanRestorePoint();

            Assert.Equal(2, backupTask.RestorePoints.Count);
        }

        [Fact]
        public void CleaningRestorePointsForHybridChooser()
        {
            DecoratorBackupTask backupTask = Initialize();

            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint1"));
            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint2"));
            Thread.Sleep(5000);
            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint3"));
            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint4"));

            DateTime dateTime = backupTask.Backup.RestorePoints[3].DateTimeOfCreation;
            backupTask.AddChooserRestorePoint(new HybridChooser(backupTask.Backup, 3, dateTime.AddSeconds(-3), RemoveMethod.OneSign, new Remover()));

            backupTask.CleanRestorePoint();

            Assert.Equal(2, backupTask.RestorePoints.Count);

            Thread.Sleep(5000);
            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint5"));
            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint6"));

            dateTime = backupTask.Backup.RestorePoints[3].DateTimeOfCreation;
            backupTask.AddChooserRestorePoint(new HybridChooser(backupTask.Backup, 3, dateTime.AddSeconds(-3), RemoveMethod.AllSign, new Remover()));

            backupTask.CleanRestorePoint();

            Assert.Equal(3, backupTask.RestorePoints.Count);
        }

        [Fact]
        public void RestoreFileToOriginalLocation()
        {
            var repository = new AdvancedMemoryRepository(@"/tmp");
            var logger = new FileLogger(repository, "log.txt");
            var backupTask = new DecoratorBackupTask(repository, "BackupTask1", new ZipArchiver(), new SplitStorageAlgorithm(), logger);

            AddBackupFile(backupTask, repository, "File1.txt");

            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint1"));

            repository.RemoveFile("File1.txt");

            Assert.False(repository.FileExist("File1.txt"));

            backupTask.Restore(backupTask.RestorePoints[0], new Restorer());

            Assert.True(repository.FileExist("File1.txt"));
        }

        [Fact]
        public void RestoreFileToDifferentLocation()
        {
            var repository = new AdvancedMemoryRepository(@"/tmp");
            var logger = new FileLogger(repository, "log.txt");
            var backupTask = new DecoratorBackupTask(repository, "BackupTask1", new ZipArchiver(), new SplitStorageAlgorithm(), logger);

            AddBackupFile(backupTask, repository, "File1.txt");

            backupTask.AddRestorePoint(backupTask.CreateRestorePoint("RestorePoint1"));

            repository.RemoveFile("File1.txt");

            Assert.False(repository.FileExist("File1.txt"));

            backupTask.Restore(backupTask.RestorePoints[0], new Restorer(), (IAdvancedRepository)repository.StepForward("TestDirectory"));

            Assert.True(repository.FileExist("TestDirectory/File1.txt"));
        }

        private DecoratorBackupTask Initialize()
        {
            var repository = new AdvancedMemoryRepository(@"/tmp");
            var logger = new FileLogger(repository, "log.txt");
            var backupTask = new DecoratorBackupTask(repository, "BackupTask1", new ZipArchiver(), new SplitStorageAlgorithm(), logger);

            AddBackupFile(backupTask, repository, "File1.txt");

            return backupTask;
        }

        private void AddBackupFile(IBackupTask backupTask, IAdvancedRepository repository, string nameFile)
        {
            using (Stream memoryStream = repository.WriteFile(nameFile))
            {
                using (var writer = new StreamWriter(memoryStream))
                {
                    writer.Write("File1");
                }
            }

            backupTask.AddBackupObject(new AdvancedBackupObjectFile(repository, nameFile));
        }
    }
}
