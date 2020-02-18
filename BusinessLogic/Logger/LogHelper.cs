using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    public static class LogHelper
    {
        private static ILogger logger;

        private static string file = "log.txt";
        static LogHelper()
        {
            logger = new FileLogger(file);
        }

        public static void Log(string message)
        { 
            logger.Log(message);
        }
    }
}
