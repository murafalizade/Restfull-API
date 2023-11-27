using System.Reflection;
using Application.Abstractions;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure;

public static class InfrastructureRegistration
{
    public static void AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
    }   
}