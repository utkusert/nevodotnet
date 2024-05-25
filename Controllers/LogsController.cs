using Microsoft.AspNetCore.Mvc;
using LogLibrary.Loggers;
using LogLibrary.Appenders;
using System.Collections.Generic;
using System.Linq;
using LogLibrary.Enums;

namespace LoggingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly LogLibrary.Loggers.ILogger logger;
        private readonly IEnumerable<IAppender> appenders;

        public LogsController(LogLibrary.Loggers.ILogger logger, IEnumerable<IAppender> appenders)
        {
            this.logger = logger;
            this.appenders = appenders;
        }

        [HttpPost]
        public IActionResult LogMessages([FromBody] List<LogMessage> logMessages)
        {
            foreach (var logMessage in logMessages)
            {
                switch (logMessage?.Level?.ToUpper())
                {
                    case "INFO":
                        logger.Info(logMessage.Time, logMessage.Message);
                        break;
                    case "WARNING":
                        logger.Warn(logMessage.Time, logMessage.Message);
                        break;
                    case "ERROR":
                        logger.Error(logMessage.Time, logMessage.Message);
                        break;
                    case "CRITICAL":
                        logger.Critical(logMessage.Time, logMessage.Message);
                        break;
                    case "FATAL":
                        logger.Fatal(logMessage.Time, logMessage.Message);
                        break;
                    default:
                        return BadRequest("Invalid log level");
                }
            }

            var response = new
            {
                ConsoleLogs = appenders.OfType<ConsoleAppender>().Select(a => new
                {
                    Type = "ConsoleAppender",
                    LayoutType = a.Layout.GetType().Name,
                    ReportLevel = a.ReportLevel.ToString(),
                    MessagesAppended = logMessages.Count(l => Enum.Parse<ReportLevel>(l.Level, true) >= a.ReportLevel)
                }),
                FileLogs = appenders.OfType<FileAppender>().Select(a => new
                {
                    Type = "FileAppender",
                    LayoutType = a.Layout.GetType().Name,
                    ReportLevel = a.ReportLevel.ToString(),
                    MessagesAppended = logMessages.Count(l => Enum.Parse<ReportLevel>(l.Level, true) >= a.ReportLevel),
                    LogContent = a.GetLogContent(),
                    FileSize = a.GetLogContent().Where(char.IsLetter).Sum(c => (int)c)
                })
            };

            return Ok(response);
        }
    }

    public class LogMessage
    {
        public string? Level { get; set; }
        public string? Time { get; set; }
        public string? Message { get; set; }
    }
}
