using FluentValidation;
using Grpc.Core;

namespace GrpcApi.Interceptor;

public class ValidationInterceptor(IServiceProvider serviceProvider) : Grpc.Core.Interceptors.Interceptor
{
    public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        var validator = serviceProvider.GetService<IValidator<TRequest>>();
        var validationResult = validator?.Validate(request);
        if (validationResult is not null && !validationResult.IsValid)
        {
            throw new RpcException(
                new Status(
                    StatusCode.InvalidArgument,
                    $"{string.Join(';', validationResult.Errors.Select(e => e.ErrorMessage))}"
                ));
        }

        return continuation(request, context);
    }
}