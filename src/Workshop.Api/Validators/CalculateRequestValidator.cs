using FluentValidation;
using FluentValidation.Validators;
using Workshop.Api.Requests.V4;

namespace Workshop.Api.Validators;

public class CalculateRequestValidator : AbstractValidator<CalculateRequest>
{
    public CalculateRequestValidator()
    {
        RuleFor(request => request.UserId)
            .GreaterThan(0);

        RuleFor(request => request.Goods)
            .NotEmpty();
        
        RuleForEach(request => request.Goods)
            .SetValidator(new GoodPropertiesValidator());
    }
}