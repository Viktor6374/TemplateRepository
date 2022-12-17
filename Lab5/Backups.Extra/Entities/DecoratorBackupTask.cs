using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backups.Algorithms;
using Backups.Entities;
using Backups.Extra.Algorithms;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Entities
{
    public class DecoratorBackupTask : IBackupTask
    {
        private BackupTask _backupTask;
        public DecoratorBackupTask(IAdvancedRepository repository, string name, IArchiver archiver, IStorageAlgorithm storageAlgorithm, ILogger logger)
        {
            _backupTask = new BackupTask(repository, name, archiver, storageAlgorithm);
            Logger = logger ?? throw new ArgumentNullException();
        }

        public IChooserRestorePoint? ChooserRestorePoint { get; private set; } = null;
        public ILogger Logger { get; }
        public string Name => _backupTask.Name;
        public Backup Backup => _backupTask.Backup;
        public IRepository Repository => _backupTask.Repository;
        public IReadOnlyList<IRestorePoint> RestorePoints => _backupTask.RestorePoints;
        public IReadOnlyList<IBackupObject> BackupObjects => _backupTask.BackupObjects;

        public void AddChooserRestorePoint(IChooserRestorePoint chooser)
        {
            if (chooser.Backup != _backupTask.Backup)
                throw new ArgumentException("Backups chooser and BackupTask don't match");

            ChooserRestorePoint = chooser ?? throw new ArgumentNullException();
        }

        public void AddBackupObject(IBackupObject backupObject)
        {
            if (!(backupObject is IAdvancedBackupObject))
                throw new ArgumentException("Invalid type Backup object");
            _backupTask.AddBackupObject(backupObject);
            Logger.CreateLog($"Add {InformationBackupObject((IAdvancedBackupObject)backupObject)}");
        }

        public void RemoveBackupObject(IBackupObject backupObject)
        {
            if (!(backupObject is IAdvancedBackupObject))
                throw new ArgumentException("Invalid type Backup object");
            _backupTask.RemoveBackupObject(backupObject);
            Logger.CreateLog($"Remove {InformationBackupObject((IAdvancedBackupObject)backupObject)}");
        }

        public AdvancedRestorePoint CreateRestorePoint(string name)
        {
            var backupObjects = new List<IBackupObject>();
            backupObjects.AddRange(BackupObjects);

            IStorage storage = Backup.StorageAlgorithm.CreateStorage(Backup.Archiver, backupObjects, Repository, name);

            var result = new AdvancedRestorePoint(storage, name, (IAdvancedRepository)Repository.StepForward(name));
            Logger.CreateLog($"Create {InformationRestorePoint(result)}");

            return result;
        }

        public void AddRestorePoint(IRestorePoint restorePoint)
        {
            _backupTask.AddRestorePoint(restorePoint);
            Logger.CreateLog($"Add {InformationRestorePoint(restorePoint)}");
        }

        public void RemoveRestorePoint(IRestorePoint restorePoint)
        {
            _backupTask.RemoveRestorePoint(restorePoint);
            Logger.CreateLog($"Remove {InformationRestorePoint(restorePoint)}");
        }

        public void Restore(IRestorePoint restorePoint, IRestorer restorer, IAdvancedRepository? repository = null)
        {
            if (_backupTask.Backup.StorageAlgorithm is SingleStorageAlgorithm)
            {
                restorer.Restore(restorePoint, new SingleRestoreAlgorithm(), repository);
            }
            else if (_backupTask.Backup.StorageAlgorithm is SplitStorageAlgorithm)
            {
                restorer.Restore(restorePoint, new SplitRestoreAlgorithm(), repository);
            }
            else
            {
                throw new ArgumentException("Unknown type Storage Algorithm");
            }

            Logger.CreateLog($"Restored {InformationRestorePoint(restorePoint)}");
        }

        public void CleanRestorePoint()
        {
            if (ChooserRestorePoint == null)
                throw new Exception("Please add Chooser Restore Point");
            ChooserRestorePoint.CleanRestorePoints();
            Logger.CreateLog($"Cleaning has been performed Rectore Points");
        }

        private string InformationBackupObject(IAdvancedBackupObject backupObject)
        {
            if (backupObject is BackupObjectFile)
            {
                var file = (BackupObjectFile)backupObject;
                return $"BackupObject, type: file, path: {file.Repository.Path()}, name: {file.Name}";
            }
            else if (backupObject is BackupObjectDirectory)
            {
                return $"BackupObject, type: directory, path: {backupObject.Repository.Path()}";
            }
            else
            {
                throw new ArgumentException("Unknown type backup object");
            }
        }

        private string InformationRestorePoint(IRestorePoint restorePoint)
        {
            return $"RestorePoint, path: {restorePoint.Repository.Path()}, name: {restorePoint.Name}";
        }
    }
}
