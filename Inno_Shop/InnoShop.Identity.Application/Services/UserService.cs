using InnoShop.Identity.Application.Services;
using InnoShop.Identity.Domain.Entities;
using InnoShop.Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace InnoShop.Identity.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IdentityDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly string _productsApiUrl;

        public UserService(IdentityDbContext context, HttpClient httpClient, IConfiguration configuration)
        {
            _context = context;
            _httpClient = httpClient;
            _productsApiUrl = configuration["ProductsApiUrl"];
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> ChangeStatusAsync(Guid userId, bool isActive)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.IsActive = isActive;
            await _context.SaveChangesAsync();

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            using var client = new HttpClient(handler);

            try
            {
                var url = $"{_productsApiUrl}/api/internal/user/{userId}/visibility?isVisible={isActive.ToString().ToLower()}";

                var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync(url, content);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CRITICAL ERROR contacting Product Service: {ex.Message}");
            }

            return true;
        }
    }
}