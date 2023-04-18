using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Vonage.Logger;

public class LogProvider
{
    private static IDictionary<string, ILogger> _loggers = new ConcurrentDictionary<string, ILogger>();
    private static ILoggerFactory _loggerFactory = new LoggerFactory();

    public static ILogger GetLogger(string category)
    {
        if (!_loggers.ContainsKey(category))
        {
            _loggers[category] = _loggerFactory?.CreateLogger(category) ?? NullLogger.Instance;
        }

        return _loggers[category];
    }

    public static void SetLogFactory(ILoggerFactory factory)
    {
        _loggerFactory?.Dispose();
        _loggerFactory = factory;
        _loggers.Clear();
    }
}