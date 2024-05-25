using LogLibrary.Enums;
using LogLibrary.Layouts;
using LogLibrary.Models;
using System;

namespace LogLibrary.Appenders
{
    public class FileAppender : IAppender
    {
        private readonly ILayout layout;
        private readonly LogFile logFile;
        private ReportLevel reportLevel;

        public FileAppender(ILayout layout)
        {
            this.layout = layout;
            this.logFile = new LogFile();
            this.ReportLevel = ReportLevel.Info;
        }

        public ReportLevel ReportLevel
        {
            get => reportLevel;
            set => reportLevel = value;
        }

        public ILayout Layout => this.layout;

        public void Append(string dateTime, string reportLevel, string message)
        {
            if (Enum.TryParse(reportLevel, out ReportLevel level) && level >= this.ReportLevel)
            {
                var formattedMessage = this.layout.Format(dateTime, reportLevel, message);
                this.logFile.Write(formattedMessage);
            }
        }

        public string GetLogContent()
        {
            return this.logFile.ReadLog();
        }
    }
}
