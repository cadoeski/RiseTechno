using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Contact.Application.Behaviors
{
    public interface ICacheable
    {
        string CacheKey { get; }
    }

    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICacheable
    {
        private readonly IMemoryCache cache;
        private readonly ILogger<CachingBehavior<TRequest, TResponse>> logger;
        public CachingBehavior(IMemoryCache cache, ILogger<CachingBehavior<TRequest, TResponse>> logger)
        {
            this.cache = cache;
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = request.GetType();
            logger.LogInformation("{Request} is configured for caching.", requestName);

            // Check to see if the item is inside the cache
            TResponse response;
            if (cache.TryGetValue(request.CacheKey, out response))
            {
                logger.LogInformation("Returning cached value for {Request}.", requestName);
                return response;
            }

            // Item is not in the cache, execute request and add to cache
            logger.LogInformation("{Request} Cache Key: {Key} is not inside the cache, executing request.", requestName, request.CacheKey);
            response = await next();
            cache.Set(request.CacheKey, response);
            return response;
        }
    }
}
