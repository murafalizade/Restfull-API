﻿namespace Application.Abstractions;

public interface IAuthService
{
    Task<string> LoginAsync(string email, string password);
    Task<string> RegisterAsync(string email, string password);
    Task<string> ForgotPasswordAsync(string email);
}