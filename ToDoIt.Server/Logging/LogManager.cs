using Serilog;
using ILogger = Serilog.ILogger;

namespace ToDoIt.Server.Logging;

public class LogManager
{
    public static ILogger GetLogger(string name)
    {
        return Log.ForContext("Source", name);
    }
}