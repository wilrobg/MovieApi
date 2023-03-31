using Microsoft.AspNetCore.Identity;
using Movies.Core.Entities;

namespace Movies.Core.Repositories;

public class UserRepository : IUserRepository
{
	private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public UserRepository(
        UserManager<User> userManager, 
        RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> CreateUser(User user, string password, string role)
    {
        var result = await _userManager.CreateAsync(user);
        
        if (!result.Succeeded) return result;

        result = await _userManager.AddPasswordAsync(user, password);

        if (!result.Succeeded) return result;

        result = await _userManager.AddToRoleAsync(user, role);

        return result;
    }

    public Task<bool> CheckPassword(User user, string password) => _userManager.CheckPasswordAsync(user, password);
    public Task<User> GetUserByEmail(User user) => _userManager.FindByEmailAsync(user.Email);
    public Task<User> GetUserByEmail(string email) => _userManager.FindByEmailAsync(email);
    public Task<IdentityResult> AddUserRole(User user, string role) => _userManager.AddToRoleAsync(user, role);
    public Task<IdentityResult> RemoveFromRole(User user, string role) => _userManager.RemoveFromRoleAsync(user, role);

    public Task<IdentityResult> AddRole(Role role)
    {
        return _roleManager.CreateAsync(role);
    }

    public async Task<IList<string>> GetUserRoles(User user)
    {
        return await _userManager.GetRolesAsync(user);
    }
}
