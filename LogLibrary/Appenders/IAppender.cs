using System;
using LogLibrary.Enums;

namespace LogLibrary.Appenders
{
	public interface IAppender
    {
        void Append(string dateTime, string reportLevel, string message);
        ReportLevel ReportLevel { get; set; }
    }
}

