namespace Backups.Interfaces
{
    public interface IArchiver
    {
        public string Archive(List<IBackupObject> backupObjects, IRepository repository, string nameFile);
    }
}
