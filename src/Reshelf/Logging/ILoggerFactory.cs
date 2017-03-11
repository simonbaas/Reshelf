using System;

namespace Reshelf.Logging
{
    public interface ILoggerFactory
    {
        ILogger GetLogger(Type type);
        ILogger GetLogger<T>();
    }
}
