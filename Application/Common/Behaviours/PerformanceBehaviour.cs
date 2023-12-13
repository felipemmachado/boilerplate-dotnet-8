using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.Common.Behaviours;
public class PerformanceBehaviour<TRequest, TResponse>(
    ILogger<TRequest> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer = new Stopwatch();
    private readonly ILogger<TRequest> _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        // 5 segundos
        if (elapsedMilliseconds > 5000)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogWarning("Orders Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                requestName, elapsedMilliseconds, request);
        }

        return response;
    }
}

