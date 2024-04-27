using System.Collections.Concurrent;
using Domain.Model;
using Domain.Repository;
using Infrastructure.Exception;

namespace Infrastructure.Repository;

public sealed class MemoryProductRepository: IProductRepository
{
    private static long _id;
    private readonly ConcurrentDictionary<long, Product> _products = new();

    public void Add(Product product)
    {
        product.Id = Interlocked.Increment(ref _id);
        
        if (!_products.TryAdd(product.Id, product))
        {
            throw new ProductException($"Product with id = {product.Id} already exists");
        }
    }

    public Product GetById(long id)
    {
        if (!_products.TryGetValue(id, out var product))
        {
            throw new ProductException($"Product with id = {id} doesn't exist");
        }

        return product;
    }

    public void Update(Product product, Product oldProduct)
    {
        if (!_products.TryUpdate(product.Id, product, oldProduct))
        {
            throw new ProductException($"Product with id = {product.Id} doesn't exists or cannot be update");
        }
    }

    public IEnumerable<Product> GetByFilter(Func<Product, bool> filter)
    {
        return _products.Values.Where(filter);
    }

    public IEnumerable<Product> GetAll()
    {
        return _products.Values;
    }
}