using FluentValidation;
using Workshop.Api.Requests.V4;

namespace Workshop.Api.Validators;

public class GetHistoryRequestValidator : AbstractValidator<GetHistoryRequest>
{
    public GetHistoryRequestValidator()
    {
        RuleFor(request => request.UserId)
            .GreaterThan(0);

        RuleFor(request => request.Take)
            .GreaterThan(0);
    }
}