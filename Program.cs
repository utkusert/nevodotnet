using Microsoft.OpenApi.Models;
using LogLibrary.Loggers;
using LogLibrary.Layouts;
using LogLibrary.Appenders;
using LogLibrary.Enums;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LoggingAPI", Version = "v1" });
});

builder.Services.AddSingleton<LogLibrary.Loggers.ILogger, Logger>(sp =>
{
    var simpleLayout = new SimpleLayout();
    var consoleAppender = new ConsoleAppender(simpleLayout) { ReportLevel = ReportLevel.Critical };
    var xmlLayout = new XmlLayout();
    var fileAppender = new FileAppender(xmlLayout) { ReportLevel = ReportLevel.Info };
    return new Logger(consoleAppender, fileAppender);
});

builder.Services.AddSingleton<IEnumerable<IAppender>>(sp =>
{
    var simpleLayout = new SimpleLayout();
    var consoleAppender = new ConsoleAppender(simpleLayout) { ReportLevel = ReportLevel.Critical };
    var xmlLayout = new XmlLayout();
    var fileAppender = new FileAppender(xmlLayout) { ReportLevel = ReportLevel.Info };
    return new List<IAppender> { consoleAppender, fileAppender };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoggingAPI v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
