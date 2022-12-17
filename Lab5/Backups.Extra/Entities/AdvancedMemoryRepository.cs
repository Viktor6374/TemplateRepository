using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;
using Zio;
using Zio.FileSystems;

namespace Backups.Extra.Entities
{
    public class AdvancedMemoryRepository : IAdvancedRepository, IDisposable
    {
        private MemoryRepository _memoryRepository;
        public AdvancedMemoryRepository(string path_, MemoryFileSystem fileSystem_)
        {
            _memoryRepository = new MemoryRepository(path_, fileSystem_);
        }

        public AdvancedMemoryRepository(string path_)
        {
            _memoryRepository = new MemoryRepository(path_);
        }

        public MemoryFileSystem FileSystem => _memoryRepository.FileSystem;
        public string Path() => _memoryRepository.Path();
        public void Dispose()
        {
            _memoryRepository.Dispose();
        }

        public IRepository StepForward(string relativePath)
        {
            return new AdvancedMemoryRepository(UPath.Combine(new UPath(Path()), new UPath(relativePath)).ToString(), FileSystem);
        }

        public void CreateDirectory(string relativePath)
        {
            _memoryRepository.CreateDirectory(relativePath);
        }

        public List<string> GetAllFile()
        {
            return _memoryRepository.GetAllFile();
        }

        public Stream ReadFile(string relativePath)
        {
            return _memoryRepository.ReadFile(relativePath);
        }

        public Stream WriteFile(string relativePath)
        {
            return _memoryRepository.WriteFile(relativePath);
        }

        public Stream OnlyWriteFile(string relativePath)
        {
            return File.Open(UPath.Combine(Path(), relativePath).ToString(), FileMode.OpenOrCreate, FileAccess.Write);
        }

        public void RemoveDirectory(string relativePath)
        {
            FileSystem.DeleteDirectory(UPath.Combine(new UPath(Path()), new UPath(relativePath)), true);
        }

        public void RemoveFile(string relativePath)
        {
            FileSystem.DeleteFile(UPath.Combine(new UPath(Path()), new UPath(relativePath)));
        }

        public long GetSizeFile(string relativePath)
        {
            return FileSystem.GetFileLength(UPath.Combine(new UPath(Path()), new UPath(relativePath)));
        }

        public void MoveFile(string relativePath, string targetFullPath)
        {
            FileSystem.MoveFile(UPath.Combine(new UPath(Path()), new UPath(relativePath)), new UPath(targetFullPath));
        }

        public bool FileExist(string relativePath)
        {
            return FileSystem.FileExists(UPath.Combine(new UPath(Path()), new UPath(relativePath)));
        }

        public void CreateFile(string relativePath)
        {
            FileSystem.CreateFile(UPath.Combine(new UPath(Path()), new UPath(relativePath)));
        }
    }
}
