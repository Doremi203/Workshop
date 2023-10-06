using FluentValidation;
using Workshop.Api.Requests.V4;

namespace Workshop.Api.Validators;

public class GoodPropertiesValidator : AbstractValidator<GoodProperties>
{
    public GoodPropertiesValidator()
    {
        RuleFor(request => request.Length)
            .GreaterThan(0);
        
        RuleFor(request => request.Width)
            .GreaterThan(0);
        
        RuleFor(request => request.Height)
            .GreaterThan(0);
        
        RuleFor(request => request.Weight)
            .GreaterThan(0);
    }
}