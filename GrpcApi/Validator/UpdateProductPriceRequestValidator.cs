using FluentValidation;

namespace GrpcApi.Validator;

public class UpdateProductPriceRequestValidator : AbstractValidator<UpdateProductPriceRequest>
{
    public UpdateProductPriceRequestValidator()
    {
        RuleFor(request => request.Id).GreaterThan(0);
        RuleFor(request => request.Price).GreaterThanOrEqualTo(0);
    }
}