using Application.Dto;
using Application.Service;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcApi.Services;

public class ProductServiceGrpc(IProductService productService) : ProductGrpc.ProductGrpcBase
{
    public override Task<AddProductResponse> AddProduct(AddProductRequest request, ServerCallContext context)
    {
        var productDto = new ProductDto
        (
            request.Name,
            request.Price,
            request.Weight,
            (Domain.Model.ProductType)request.Type,
            request.CreationDate.ToDateTime(),
            request.WarehouseId
        );
        var id = productService.Add(productDto);

        return Task.FromResult(new AddProductResponse
            {
                Product = new Product
                {
                    Id = id,
                    Name = request.Name,
                    Price = request.Price,
                    Weight = request.Weight,
                    Type = request.Type,
                    CreationDate = request.CreationDate,
                    WarehouseId = request.WarehouseId
                }
            }
        );
    }

    public override Task<GetProductResponse> GetProduct(GetProductRequest request, ServerCallContext context)
    {
        var product = productService.GetById(request.Id);

        return Task.FromResult(new GetProductResponse
            {
                Product = new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Weight = product.Weight,
                    Type = (ProductType)product.ProductKind,
                    CreationDate = product.CreationDate.ToTimestamp(),
                    WarehouseId = product.WarehouseId
                }
            }
        );
    }

    public override Task<ListProductsResponse> ListProducts(ListProductsRequest request, ServerCallContext context)
    {
        var products =
            productService.GetByFilter
            (
                request.CreationDate?.ToDateTime(), 
                (Domain.Model.ProductType)request.Type,
                request.WarehouseId,
                request.PageNumber,
                request.PageSize
            );

        return Task.FromResult(new ListProductsResponse
        {
            Products =
            {
                products.Select(product => new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Weight = product.Weight,
                    Type = (ProductType)product.ProductKind,
                    CreationDate = product.CreationDate.ToTimestamp(),
                    WarehouseId = product.WarehouseId
                })
            }
        });
    }

    public override Task<UpdateProductPriceResponse> UpdateProductPrice(UpdateProductPriceRequest request, ServerCallContext context)
    {
        productService.UpdatePrice(request.Id, request.Price);
        var product = productService.GetById(request.Id);

        return Task.FromResult(new UpdateProductPriceResponse
            {
                Product = new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Weight = product.Weight,
                    Type = (ProductType)product.ProductKind,
                    CreationDate = product.CreationDate.ToTimestamp(),
                    WarehouseId = product.WarehouseId
                }
            }
        );
    }
}