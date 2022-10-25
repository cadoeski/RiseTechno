using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Application.Behaviors
{
    public class TimingMetricBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
      where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TimingMetricBehavior<TRequest, TResponse>> logger;

        public TimingMetricBehavior(ILogger<TimingMetricBehavior<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = request.GetType();
            logger.LogDebug("{Request} is starting.", requestName);
            var timer = Stopwatch.StartNew();
            var response = await next();
            timer.Stop();
            logger.LogDebug("{Request} has finished in {Time}ms", requestName, timer.ElapsedMilliseconds);

            return response;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = request.GetType();
            logger.LogDebug("{Request} is starting.", requestName);
            var timer = Stopwatch.StartNew();
            var response = await next();
            timer.Stop();
            logger.LogDebug("{Request} has finished in {Time}ms", requestName, timer.ElapsedMilliseconds);

            return response;
        }
    }
}
