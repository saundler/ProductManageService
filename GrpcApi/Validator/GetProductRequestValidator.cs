using FluentValidation;

namespace GrpcApi.Validator;

public class GetProductRequestValidator : AbstractValidator<GetProductRequest>
{
    public GetProductRequestValidator()
    {
        RuleFor(request => request.Id).GreaterThan(0);
    }
}