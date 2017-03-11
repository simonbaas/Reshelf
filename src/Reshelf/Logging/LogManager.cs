using System;

namespace Reshelf.Logging
{
    internal static class LogManager
    {
        internal static ILoggerFactory LoggerFactory { get; set; } = new NullLoggerFactory();

        internal static ILogger GetLogger<T>()
        {
            return LoggerFactory.GetLogger<T>();
        }

        internal static ILogger GetLogger(Type type)
        {
            return LoggerFactory.GetLogger(type);
        }
    }
}
