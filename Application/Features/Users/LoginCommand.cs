using Application.Abstractions;
using MediatR;

namespace Application.Features.Users;

public class LoginCommand: IRequest<string>
{
    public string Email { get; set; }  
    public string Password { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        string token = await _authService.LoginAsync(request.Email, request.Password);
        return token;
    }
}
