using System;
using System.IO;

namespace Logger
{
    public class FileLogger : ILogger
    {
        private readonly string logFile;

        public FileLogger(string logFile)
        {
            this.logFile = logFile ?? throw new ArgumentNullException(nameof(logFile));
        }

        public void Log(string message)
        {
            using(StreamWriter writer = new StreamWriter(logFile,true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
    }
}
