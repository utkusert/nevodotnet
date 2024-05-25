using LogLibrary.Enums;
using LogLibrary.Layouts;
using System;

namespace LogLibrary.Appenders
{
    public class ConsoleAppender : IAppender
    {
        private readonly ILayout layout;
        private ReportLevel reportLevel;

        public ConsoleAppender(ILayout layout)
        {
            this.layout = layout;
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
                Console.WriteLine(this.layout.Format(dateTime, reportLevel, message));
            }
        }
    }
}
