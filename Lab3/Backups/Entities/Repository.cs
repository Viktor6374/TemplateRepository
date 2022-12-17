using Backups.Interfaces;
using Zio;

namespace Backups.Entities
{
    public class Repository : IRepository
    {
        private UPath path;
        public Repository(string path_)
        {
            if (string.IsNullOrEmpty(path_))
            {
                throw new ArgumentNullException();
            }

            path = new UPath(path_);
        }

        public Stream ReadFile(string relativePath)
        {
            return File.OpenRead(UPath.Combine(Path(), relativePath).ToString());
        }

        public Stream WriteFile(string relativePath)
        {
            return File.Open(UPath.Combine(Path(), relativePath).ToString(), FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        public IRepository StepForward(string relativePath)
        {
            return new Repository(UPath.Combine(path, new UPath(relativePath)).ToString());
        }

        public string Path() => path.ToString();
        public List<string> GetAllFile()
        {
            var namesOfFile = new List<string>();

            foreach (string subdirectory in Directory.GetDirectories(Path()))
            {
                var repository = new Repository(subdirectory);
                namesOfFile.AddRange(repository.GetAllFile());
            }

            namesOfFile.AddRange(Directory.GetFiles(Path()));

            return namesOfFile;
        }

        public void CreateDirectory(string relativePath)
        {
            Directory.CreateDirectory(UPath.Combine(path, new UPath(relativePath)).ToString());
        }
    }
}
