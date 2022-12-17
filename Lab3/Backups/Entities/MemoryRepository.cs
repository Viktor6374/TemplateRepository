using Backups.Interfaces;
using Zio;
using Zio.FileSystems;

namespace Backups.Entities
{
    public class MemoryRepository : IRepository, IDisposable
    {
        private UPath path;

        public MemoryRepository(string path_)
        {
            Initialize(path_);
            FileSystem = new MemoryFileSystem();
            FileSystem.CreateDirectory(UPath.Combine(path, new UPath(path_)).ToString());
        }

        public MemoryRepository(string path_, MemoryFileSystem fileSystem_)
        {
            Initialize(path_);
            FileSystem = fileSystem_ ?? throw new ArgumentNullException();
            FileSystem.CreateDirectory(UPath.Combine(path, new UPath(path_)).ToString());
        }

        public MemoryFileSystem FileSystem { get; }
        public IRepository StepForward(string relativePath)
        {
            return new MemoryRepository(UPath.Combine(path, new UPath(relativePath)).ToString(), FileSystem);
        }

        public void CreateDirectory(string relativePath)
        {
            FileSystem.CreateDirectory(UPath.Combine(path, new UPath(relativePath)));
        }

        public List<string> GetAllFile()
        {
            var namesOfFile = new List<string>();

            foreach (string subdirectory in FileSystem.EnumerateDirectories(path))
            {
                var repository = new MemoryRepository(subdirectory, FileSystem);
                namesOfFile.AddRange(repository.GetAllFile());
            }

            foreach (UPath path in FileSystem.EnumerateFiles(path))
            {
                namesOfFile.Add(path.ToString());
            }

            return namesOfFile;
        }

        public string Path() => path.ToString();
        public Stream ReadFile(string relativePath)
        {
            return FileSystem.OpenFile(UPath.Combine(path, new UPath(relativePath)), FileMode.OpenOrCreate, FileAccess.Read);
        }

        public Stream WriteFile(string relativePath)
        {
            return FileSystem.OpenFile(UPath.Combine(path, new UPath(relativePath)), FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        public void Dispose()
        {
            FileSystem.Dispose();
        }

        private void Initialize(string path_)
        {
            if (string.IsNullOrEmpty(path_))
            {
                throw new ArgumentNullException();
            }

            path = new UPath(path_);
        }
    }
}
