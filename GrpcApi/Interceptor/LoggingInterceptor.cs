namespace GrpcApi.Interceptor;

using Grpc.Core;

public class LoggingInterceptor(ILogger<LoggingInterceptor> logger)
    : Grpc.Core.Interceptors.Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        logger.LogInformation("Getting request = {Request}", request);
        var response = await continuation(request, context);
        logger.LogInformation("Sending response = {Response}", response);

        return response;
    }
}