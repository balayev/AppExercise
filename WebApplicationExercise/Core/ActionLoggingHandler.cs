using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Tracing;

namespace WebApplicationExercise.Core
{
    public class ActionLoggingHandler : DelegatingHandler
    {
        private readonly SimpleTracer _tracer;

        public ActionLoggingHandler(SimpleTracer tracer)
        {
            _tracer = tracer;
        }

        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            _tracer.Debug(request, "Request start", $"{request.Method} {request.RequestUri.AbsoluteUri}");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var response = await base.SendAsync(request, cancellationToken);
            stopWatch.Stop();
            _tracer.Debug(request, "Request end", $"{request.Method} {request.RequestUri.AbsoluteUri} Execution time: {stopWatch.Elapsed.TotalMilliseconds} ms");

            return response;
        }
    }
}