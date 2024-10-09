using FluentValidation;
using RengieExModels.Requests;

namespace RengieExApi.Validators;

/// <summary>
/// Validator for the RegexRequest
/// </summary>
public class RegexRequestValidator : AbstractValidator<RegexRequest>
{
    public RegexRequestValidator()
    {
        RuleFor(x => x.Pattern)
            .NotEmpty();
        RuleFor(x => x.Text)
            .NotEmpty();
    }
}