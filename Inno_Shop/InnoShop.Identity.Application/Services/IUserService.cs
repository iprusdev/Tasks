using InnoShop.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InnoShop.Identity.Application.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<bool> ChangeStatusAsync(Guid userId, bool isActive);
    }
}