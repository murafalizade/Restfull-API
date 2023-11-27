using Application.Abstractions;
using Application.Validations;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Application.Features.Users;

public class RegisterCommand:IRequest<string>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var validator = new AuthValidation();
        var result = await validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
        string token = await _authService.RegisterAsync(request.Email, request.Password);
        return token;
    }
}