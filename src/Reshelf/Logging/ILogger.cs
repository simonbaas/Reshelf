using System;

namespace Reshelf.Logging
{
    public interface ILogger
    {
        void Debug(string message);
        void Debug(string format, params object[] args);
        void Debug(string message, Exception exception);
        void Info(string message);
        void Info(string format, params object[] args);
        void Info(string message, Exception exception);
        void Warning(string message);
        void Warning(string format, params object[] args);
        void Warning(string message, Exception exception);
        void Error(string message);
        void Error(string format, params object[] args);
        void Error(string message, Exception exception);
        void Fatal(string message);
        void Fatal(string format, params object[] args);
        void Fatal(string message, Exception exception);
    }
}
