using NLog;

namespace WebApplicationExercise.Core
{
    public class LoggerProvider
    {
        public ILogger Create<T>()
        {
            return LogManager.GetCurrentClassLogger(typeof(T));
        }
    }
}