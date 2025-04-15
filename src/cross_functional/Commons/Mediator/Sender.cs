namespace Commons.Mediator
{
    public sealed class Sender(IServiceProvider serviceProvider) : ISender
    {
        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = serviceProvider.GetService(handlerType);
            if (handler == null)
            {
                throw new InvalidOperationException($"Handler for {request.GetType()} not found.");
            }
            return ((dynamic)handler).Handle((dynamic)request, cancellationToken);
        }
    }
}
