using Backups.Extra.Enams;
using Backups.Extra.Interfaces;

namespace Backups.Extra.Entities
{
    public class ConsoleLogger : ILogger
    {
        public ConsoleLogger()
        {
            DefaultOptionLogger = OptionLogger.None;
        }

        public ConsoleLogger(OptionLogger optionLogger)
        {
            DefaultOptionLogger = optionLogger;
        }

        public OptionLogger DefaultOptionLogger { get; }

        public void CreateLog(string message)
        {
            CreateLog(message, DefaultOptionLogger);
        }

        public void CreateLog(string message, OptionLogger optionLogger)
        {
            if (optionLogger == OptionLogger.Default)
            {
                Console.WriteLine($"{message}\n");
            }
            else if (optionLogger == OptionLogger.TimeCode)
            {
                Console.WriteLine($"[{DateTime.Now}] {message}\n");
            }
            else if (optionLogger == OptionLogger.None)
            {
            }
            else
            {
                throw new ArgumentException("Unknown value OptionLogger");
            }
        }
    }
}
