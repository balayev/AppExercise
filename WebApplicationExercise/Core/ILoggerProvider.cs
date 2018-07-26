using System;
using NLog;

namespace WebApplicationExercise.Core
{
    public interface ILoggerProvider
    {
        ILogger Create(Type type);
        ILogger Create(string loggerName);
    }
}