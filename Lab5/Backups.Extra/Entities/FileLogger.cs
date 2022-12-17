using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backups.Extra.Enams;
using Backups.Extra.Interfaces;

namespace Backups.Extra.Entities
{
    public class FileLogger : ILogger
    {
        public FileLogger(IAdvancedRepository repository, string relativePathInRepository)
        {
            Repository = repository ?? throw new ArgumentNullException();

            RelativePathInRepository = relativePathInRepository;
            DefaultOptionLogger = OptionLogger.None;
        }

        public FileLogger(IAdvancedRepository repository, string relativePathInRepository, OptionLogger optionLogger)
        {
            Repository = repository ?? throw new ArgumentNullException();

            RelativePathInRepository = relativePathInRepository;
            DefaultOptionLogger = optionLogger;
        }

        public IAdvancedRepository Repository { get; }
        public string RelativePathInRepository { get; }
        public OptionLogger DefaultOptionLogger { get; }

        public void CreateLog(string message)
        {
            CreateLog(message, DefaultOptionLogger);
        }

        public void CreateLog(string message, OptionLogger optionLogger)
        {
            using (Stream stream = Repository.WriteFile(RelativePathInRepository))
            {
                using (var writer = new StreamWriter(stream))
                {
                    if (optionLogger == OptionLogger.Default)
                    {
                        writer.Write($"{message}\n");
                    }
                    else if (optionLogger == OptionLogger.TimeCode)
                    {
                        writer.Write($"[{DateTime.Now}] {message}\n");
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
    }
}
