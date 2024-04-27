using Application.Service;

namespace GrpcApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<IProductService, ProductService>();
    }
}