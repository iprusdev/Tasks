using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InnoShop.Identity.Application.DTOs;
using InnoShop.Identity.Domain.Entities;
using InnoShop.Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace InnoShop.Identity.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IdentityDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService; 

        public AuthService(IdentityDbContext context, IConfiguration configuration, IEmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email)) return null;

            var verificationToken = Guid.NewGuid().ToString();

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Role = "User",
                IsActive = true,
                IsEmailConfirmed = false, 
                EmailVerificationToken = verificationToken,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var baseUrl = _configuration["AppUrl"];
            var link = $"{baseUrl}/api/auth/verify-email?userId={user.Id}&token={verificationToken}";

            await _emailService.SendEmailAsync(user.Email, "InnoShop Confirmation",
                $"<h1>Welcome!</h1><p>Please confirm email:</p><a href='{link}'>Click here</a>");

            return GenerateJwtResponse(user);
        }


        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) return null;
            if (!user.IsActive) return null;
            return GenerateJwtResponse(user);
        }


        public async Task<bool> VerifyEmailAsync(Guid userId, string token)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            if (user.EmailVerificationToken != token) return false;

            user.IsEmailConfirmed = true;
            user.EmailVerificationToken = null; 
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) return "User not found";

            var resetToken = Guid.NewGuid().ToString();
            user.PasswordResetToken = resetToken;
            user.ResetTokenExpires = DateTime.UtcNow.AddHours(1);
            await _context.SaveChangesAsync();

            await _emailService.SendEmailAsync(user.Email, "Reset Password",
                $"Your reset token is: <b>{resetToken}</b>");

            return "Token sent to email";
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) return "User not found";

            if (user.PasswordResetToken != dto.Token) return "Invalid token";
            if (user.ResetTokenExpires < DateTime.UtcNow) return "Token expired";

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;
            await _context.SaveChangesAsync();

            return "Password reset successful";
        }

        private AuthResponseDto GenerateJwtResponse(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("role", user.Role)
            };
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Role = user.Role
            };
        }
    }
}