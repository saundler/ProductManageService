using Application.Dto;
using Domain.Model;
using Domain.Repository;

namespace Application.Service;

public sealed class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public long Add(ProductDto productDto)
    {
        var product = new Product
        (
            productDto.Name,
            productDto.Price,
            productDto.Weight,
            productDto.ProductKind,
            productDto.CreationDate,
            productDto.WarehouseId
        );
        
        _productRepository.Add(product);

        return product.Id;
    }

    public IEnumerable<Product> GetByFilter
    (
        DateTime? creationDate,
        ProductType? productType,
        long warehouseId,
        int pageNum,
        int pageSize
    )
    {
        var query = _productRepository.GetAll();

        if (creationDate.HasValue)
        {
            query = query.Where(product => product.CreationDate.Date == creationDate.Value.Date);
        }

        if (productType.HasValue)
        {
            query = query.Where(product => product.ProductKind == productType.Value);
        }

        if (warehouseId != 0)
        {
            query = query.Where(product => product.WarehouseId == warehouseId);
        }

        query = query.Skip((pageNum - 1) * pageSize).Take(pageSize);

        return query;
    }

    public Product GetById(long id) => _productRepository.GetById(id);

    public void UpdatePrice(long id, double newPrice)
    {
        var product = GetById(id);
        var newProduct = new Product
        (
            product.Name, 
            newPrice, 
            product.Weight, 
            product.ProductKind, 
            product.CreationDate, 
            product.WarehouseId
        )
        {
            Id = product.Id
        };

        _productRepository.Update(newProduct, product);
    }
}