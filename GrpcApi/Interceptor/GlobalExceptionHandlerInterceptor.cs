using Grpc.Core;

namespace GrpcApi.Interceptor;

public class GlobalExceptionHandlerInterceptor(ILogger<GlobalExceptionHandlerInterceptor> logger)
    : Grpc.Core.Interceptors.Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (Exception e)
        {
            logger.LogInformation(e, "GlobalExceptionHandlerInterceptor catch exception");
            
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, e.Message));
        }
    }
}