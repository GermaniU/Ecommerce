using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.Behaviors;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogError(ex, "Application Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
            throw new Exception($"Application Request: Unhandled Exception for Request {requestName}", ex);
        }
    }
}