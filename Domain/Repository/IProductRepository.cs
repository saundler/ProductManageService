using Domain.Model;

namespace Domain.Repository;

public interface IProductRepository
{
    void Add(Product product);
    Product GetById(long id);
    void Update(Product newProduct, Product oldProduct);
    IEnumerable<Product> GetByFilter(Func<Product, bool> filter);
    IEnumerable<Product> GetAll();
}