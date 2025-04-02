﻿using Distri.Shared.DTOs;
using Distri.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace Distri.Backend.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> GetUserAsync(string email);
        Task<IdentityResult> AddUserAsync(User user, string password);
        Task CheckRoleAsync(string roleName);
        Task AddUserToRoleAsync(User user, string roleName);
        Task<bool> IsUserInRoleAsync(User user, string roleName);
        Task<SignInResult> LoginAsync(LoginDTO model);
        Task LogoutAsync();
    }
}