using Backups.Interfaces;

using Zio;

namespace Backups.Entities
{
    public class BackupObjectFile : IBackupObject
    {
        public BackupObjectFile(IRepository repository, string name)
        {
            Repository = repository ?? throw new ArgumentNullException();

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException();
            }

            Name = name;
        }

        public IRepository Repository { get; }
        public string Name { get; }

        public string Path => UPath.Combine(Repository.Path(), Name).ToString();
    }
}
