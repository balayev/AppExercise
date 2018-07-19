using NLog;

namespace WebApplicationExercise.Core
{
    public interface ILoggerProvider
    {
        ILogger Create<T>();
    }
}