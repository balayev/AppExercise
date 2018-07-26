using System;
using System.Net.Http;
using System.Web.Http.Tracing;
using NLog;

namespace WebApplicationExercise.Core
{
    public class LoggerProvider : ILoggerProvider
    {
        public ILogger Create(Type type)
        {
            return LogManager.GetCurrentClassLogger(type);
        }

        public ILogger Create(string loggerName)
        {
            return LogManager.GetLogger(loggerName);
        }
    }

    public class SimpleTracer : ITraceWriter
    {
        private readonly ILoggerProvider _loggerProvider;

        public SimpleTracer(ILoggerProvider loggerProvider)
        {
            _loggerProvider = loggerProvider;
        }

        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            TraceRecord rec = new TraceRecord(request, category, level);
            traceAction(rec);
            switch (level)
            {
                case TraceLevel.Off:
                    break;
                case TraceLevel.Debug:
                    WriteDebug(rec);
                    break;
                case TraceLevel.Info:
                    WriteTrace(rec);
                    break;
                case TraceLevel.Warn:
                    break;
                case TraceLevel.Error:
                    WriteError(rec);
                    break;
                case TraceLevel.Fatal:
                    break;
                default:
                    break;
            }
        }

        protected void WriteTrace(TraceRecord rec)
        {
            var message = $"{rec.Operator};{rec.Operation};{rec.Message};";
            var logger = _loggerProvider.Create(rec.Category);
            logger.Trace(message);
        }

        protected void WriteDebug(TraceRecord rec)
        {
            var message = $"{rec.Operator};{rec.Operation};{rec.Message};";
            var logger = _loggerProvider.Create(rec.Category);
            logger.Debug(message);
        }

        protected void WriteError(TraceRecord rec)
        {
            var message = $"{rec.Operator};{rec.Operation};{rec.Message};";
            var logger = _loggerProvider.Create(rec.Category);
            logger.Error(message);
        }
    }
}