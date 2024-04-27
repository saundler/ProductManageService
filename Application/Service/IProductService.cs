using Application.Dto;
using Domain.Model;

namespace Application.Service;

public interface IProductService
{
    
    long Add(ProductDto productDto);

    IEnumerable<Product> GetByFilter
    (
        DateTime? creationDate, 
        ProductType? productType, 
        long warehouseId, 
        int pageNum,
        int pageSize
    
        );
    Product GetById(long id);
    
    void UpdatePrice(long id, double newPrice);
}
