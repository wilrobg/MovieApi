using Microsoft.AspNetCore.Identity;
using Movies.Core.Entities;

namespace Movies.Core.Repositories;

public interface IUserRepository
{
    Task<IdentityResult> CreateUser(User user, string password, string role);
    Task<User> GetUserByEmail(User user);
    Task<IdentityResult> AddRole(Role role);
}
