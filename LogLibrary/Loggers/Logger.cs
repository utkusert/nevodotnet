using LogLibrary.Appenders;
using LogLibrary.Loggers;
using System.Collections.Generic;
using System.Linq;

namespace LogLibrary.Loggers
{
    public class Logger : ILogger
    {
        private readonly List<IAppender> appenders;

        public Logger(params IAppender[] appenders)
        {
            this.appenders = appenders.ToList();
        }

        public void Log(string dateTime, string reportLevel, string message)
        {
            foreach (var appender in this.appenders)
            {
                appender.Append(dateTime, reportLevel, message);
            }
        }

        public void Info(string dateTime, string message) => Log(dateTime, "Info", message);
        public void Warn(string dateTime, string message) => Log(dateTime, "Warn", message);
        public void Error(string dateTime, string message) => Log(dateTime, "Error", message);
        public void Critical(string dateTime, string message) => Log(dateTime, "Critical", message);
        public void Fatal(string dateTime, string message) => Log(dateTime, "Fatal", message);
    }
}
