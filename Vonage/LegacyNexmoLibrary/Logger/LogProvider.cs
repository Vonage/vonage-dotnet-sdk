using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System;
namespace Nexmo.Api.Logger
{
    [System.Obsolete("The Nexmo.Api.Logger.LogProvider class is obsolete. " +
        "References to it should be switched to the new Vonage.Logger.LogProvider class.")]
    public static class LogProvider
    {
        private static IDictionary<string, ILogger> _loggers = new Dictionary<string, ILogger>();
        private static ILoggerFactory _loggerFactory = new LoggerFactory();

        public static void SetLogFactory(ILoggerFactory factory)
        {
            _loggerFactory?.Dispose();
            _loggerFactory = factory;
            _loggers.Clear();
        }

        public static ILogger GetLogger(string category)
        {
            if (!_loggers.ContainsKey(category))
            {
                _loggers[category] = _loggerFactory?.CreateLogger(category)?? NullLogger.Instance;
            }
            return _loggers[category];
        }
    }
}