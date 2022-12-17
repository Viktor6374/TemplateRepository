using Backups.Extra.Enams;

namespace Backups.Extra.Interfaces
{
    public interface ILogger
    {
        public void CreateLog(string message);
        public void CreateLog(string message, OptionLogger optionLogger);
    }
}
