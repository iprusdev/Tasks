using System.Net.Http.Json;
using System.Threading.Tasks;
using InnoShop.Identity.Application.DTOs;
using Xunit;

namespace InnoShop.Identity.Tests.IntegrationTests
{
    public class AuthControllerTests : IClassFixture<IdentityApiFactory>
    {
        private readonly IdentityApiFactory _factory;

        public AuthControllerTests(IdentityApiFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Register_Should_Return_Ok_And_Token()
        {
            var client = _factory.CreateClient(); 
            var registerDto = new RegisterDto
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "Password123!"
            };

   
            var response = await client.PostAsJsonAsync("/api/auth/register", registerDto);

            response.EnsureSuccessStatusCode(); 

            var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            Assert.NotNull(result);
            Assert.False(string.IsNullOrEmpty(result.Token)); 
            Assert.Equal("test@example.com", result.Email);
        }
    }
}