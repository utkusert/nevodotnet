using System;
namespace LogLibrary.Loggers
{
	public interface ILogger
	{
        void Info(string dateTime, string message);
        void Warn(string dateTime, string message);
        void Error(string dateTime, string message);
        void Critical(string dateTime, string message);
        void Fatal(string dateTime, string message);

    }
}

