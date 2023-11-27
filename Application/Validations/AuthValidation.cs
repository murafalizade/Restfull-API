using Application.Features.Users;
using FluentValidation;

namespace Application.Validations;

public class AuthValidation:AbstractValidator<RegisterCommand>
{
    public AuthValidation()
    {
        RuleFor(command => command.Email).NotEmpty().EmailAddress()
            .WithMessage("Düzgün mail addresi yazın zəhmət olmasa");
        RuleFor(command => command.Password).MinimumLength(6).NotEmpty()
            .WithMessage("Düzgün şifrə daxil yazın zəhmət olmasa");
    }   
}