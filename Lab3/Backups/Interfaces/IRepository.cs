namespace Backups.Interfaces
{
    public interface IRepository
    {
        public Stream ReadFile(string relativePath);
        public Stream WriteFile(string relativePath);
        public void CreateDirectory(string relativePath);
        public IRepository StepForward(string relativePath);
        public string Path();
        public List<string> GetAllFile();
    }
}
