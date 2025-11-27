using System;
using System.Threading.Tasks;
using InnoShop.Products.Application.DTOs;
using InnoShop.Products.Application.Services;
using InnoShop.Products.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InnoShop.Products.Tests.UnitTests
{
    public class ProductServiceTests
    {
        private ProductsDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;
            return new ProductsDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_Should_Add_Product_To_Database()
        {
            var context = GetInMemoryDbContext();
            var service = new ProductService(context);
            var userId = Guid.NewGuid();
            var dto = new CreateProductDto
            {
                Name = "Test Phone",
                Price = 100,
                Description = "Good phone"
            };

            var result = await service.CreateAsync(userId, dto);

            Assert.NotNull(result); 
            Assert.Equal("Test Phone", result.Name); 
            Assert.Equal(userId, result.UserId); 


            var count = await context.Products.CountAsync();
            Assert.Equal(1, count);
        }
    }
}