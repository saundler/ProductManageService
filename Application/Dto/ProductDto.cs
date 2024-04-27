using Domain.Model;

namespace Application.Dto;

public record ProductDto
(
    string Name,
    double Price,
    double Weight,
    ProductType ProductKind,
    DateTime CreationDate,
    long WarehouseId
);