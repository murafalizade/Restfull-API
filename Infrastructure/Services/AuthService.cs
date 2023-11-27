using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            if (!result.Succeeded) throw new Exception("Invalid login attempt");
            var user = await _userManager.FindByEmailAsync(email);
            await _userManager.AddToRoleAsync(user, "admin");
            return TokenGenerator(user);
        }

        public async Task<string> RegisterAsync(string email, string password)
        {
            var user = new ApplicationUser { UserName = email, Email = email, Role = "admin" };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return TokenGenerator(user);
            }
            throw new Exception("Failed to register user");
        }

        public async Task<string> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }

        private string TokenGenerator(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretToken = _configuration.GetSection("Authentication:SecretKey").Value ?? throw new Exception("SecretKey not configured");
            var expirationDays = int.Parse(_configuration.GetSection("Authentication:ExpireDate").Value ?? "0");

            var key = Encoding.ASCII.GetBytes(secretToken);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("Version", "1")
                }),
                Expires = DateTime.UtcNow.AddDays(expirationDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
