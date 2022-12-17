using Backups.Interfaces;

namespace Backups.Extra.Interfaces
{
    public interface IAdvancedRepository : IRepository
    {
        public void RemoveDirectory(string relativePath);
        public void RemoveFile(string relativePath);
        public long GetSizeFile(string relativePath);
        public void MoveFile(string relativePath, string targetFullPath);
        public bool FileExist(string relativePath);
        public void CreateFile(string relativePath);
        public Stream OnlyWriteFile(string relativePath);
    }
}
