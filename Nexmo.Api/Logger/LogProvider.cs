using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
namespace Nexmo.Api.Logger
{
    public static class LogProvider
    {
        private static IDictionary<string, ILogger> _loggers = new Dictionary<string, ILogger>();
        private static ILoggerFactory _loggerFactory = new LoggerFactory();

        public static void SetLogFactory(ILoggerFactory factory)
        {
            _loggerFactory.Dispose();
            _loggerFactory = factory;
            _loggers.Clear();
        }

        public static ILogger GetLogger(string category)
        {
            if (!_loggers.ContainsKey(category))
            {
                _loggers[category] = _loggerFactory.CreateLogger(category);
            }
            return _loggers[category];
        }
    }
}