using FluentValidation;

namespace GrpcApi.Validator;

public class AddProductRequestValidator : AbstractValidator<AddProductRequest>
{
    public AddProductRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Price).GreaterThanOrEqualTo(0);
        RuleFor(request => request.Weight).GreaterThanOrEqualTo(0);
        RuleFor(request => request.Type).NotNull();
        RuleFor(request => request.WarehouseId).GreaterThan(0);
        RuleFor(request => request.CreationDate).NotNull();
    }
}