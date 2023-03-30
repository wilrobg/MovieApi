﻿using Microsoft.AspNetCore.Identity;
using Movies.Core.Entities;

namespace Movies.Core.Repositories;

public interface IUserRepository
{
    Task<IdentityResult> CreateUser(User user, string password, string role);
    Task<bool> CheckPassword(User user, string password);
    Task<User> GetUserByEmail(User user);
    Task<User> GetUserByEmail(string email);
    Task<IdentityResult> AddRole(Role role);
}
