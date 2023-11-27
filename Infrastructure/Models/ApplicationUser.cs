using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models;

public class ApplicationUser:IdentityUser
{
    public string Role { get; set; }
}