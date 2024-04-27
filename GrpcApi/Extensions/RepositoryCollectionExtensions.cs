using Domain.Repository;
using Infrastructure.Repository;

namespace GrpcApi.Extensions;

public static class RepositoryCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<IProductRepository, MemoryProductRepository>();
    }
}