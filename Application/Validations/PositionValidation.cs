using Application.Features.Positions;
using FluentValidation;

namespace Application.Validations;

public class PositionValidation:AbstractValidator<CreatePositionCommand>
{
    public PositionValidation()
    {
        RuleFor(command => command.Name).NotEmpty()
            .MaximumLength(50).WithMessage("Ad boş ola bilməz!");
        RuleFor(command => command.Salary).NotNull().GreaterThanOrEqualTo(0)
            .WithMessage("Maaş mənfi dəyərdə və ya boş ola bilməz!");
    }
}