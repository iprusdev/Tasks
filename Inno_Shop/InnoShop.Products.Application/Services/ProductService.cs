using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InnoShop.Products.Application.DTOs;
using InnoShop.Products.Application.Services;
using InnoShop.Products.Domain.Entities;
using InnoShop.Products.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.Products.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductsDbContext _context;

        public ProductService(ProductsDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDto> CreateAsync(Guid userId, CreateProductDto dto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                UserId = userId, 
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return MapToDto(product);
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _context.Products
                                         .Where(p => p.IsAvailable)
                                         .ToListAsync();

            return products.Select(MapToDto).ToList();
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null || !product.IsAvailable)
                return null;

            return MapToDto(product);
        }

        public async Task<bool> UpdateAsync(Guid userId, Guid productId, UpdateProductDto dto)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null) return false; 

  
            if (product.UserId != userId) return false;

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.IsAvailable = dto.IsAvailable;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid userId, Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null) return false;
            if (product.UserId != userId) return false;
            product.IsDeleted = true;

            await _context.SaveChangesAsync();
            return true;
        }

        private static ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                UserId = product.UserId,
                CreatedAt = product.CreatedAt
            };
        }
        public async Task<bool> SwitchUserProductsAvailabilityAsync(Guid userId, bool isAvailable)
        {

            var products = await _context.Products
                .Where(p => p.UserId == userId)
                .ToListAsync();

            if (!products.Any()) return false;

            foreach (var p in products)
            {
                p.IsAvailable = isAvailable;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }

}