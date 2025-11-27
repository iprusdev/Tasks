using InnoShop.Products.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InnoShop.Products.Application.Services
{
    public interface IProductService
    {
        Task<ProductDto> CreateAsync(Guid userId, CreateProductDto dto);

        Task<List<ProductDto>> GetAllAsync();

        Task<ProductDto> GetByIdAsync(Guid id);

        Task<bool> UpdateAsync(Guid userId, Guid productId, UpdateProductDto dto);

        Task<bool> DeleteAsync(Guid userId, Guid productId);
        Task<bool> SwitchUserProductsAvailabilityAsync(Guid userId, bool isAvailable);
    }
}