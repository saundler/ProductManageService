namespace Domain.Model;

public sealed class Product
{
    public long Id { get; set; }
    public string Name { get; init; }
    public double Price { get; init; }
    public double Weight { get; init; }
    public ProductType ProductKind { get; init; }
    public DateTime CreationDate { get; init; }
    public long WarehouseId { get; set; }

    public Product
    (
        string name,
        double price,
        double weight,
        ProductType productType,
        DateTime creationDate,
        long warehouseId
    )
    {
        if (name.Length < 3)
        {
            throw new ArgumentException("Length of name must be at least 3.");
        }
        
        Name = name;
        Price = price;
        Weight = weight;
        ProductKind = productType;
        CreationDate = creationDate;
        WarehouseId = warehouseId;
    }
}
