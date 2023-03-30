using Microsoft.AspNetCore.Identity;
using Movies.Core.Entities;

namespace Movies.Core.Repositories;

public class UserRepository : IUserRepository
{
	private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<Role> _roleManager;

    public UserRepository(
        SignInManager<User> signInManager, 
        RoleManager<Role> roleManager)
    {
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> CreateUser(User user, string password, string role)
    {
        var result = await _signInManager.UserManager.CreateAsync(user);
        
        if (!result.Succeeded) return result;

        result = await _signInManager.UserManager.AddPasswordAsync(user, password);

        if (!result.Succeeded) return result;

        result = await _signInManager.UserManager.AddToRoleAsync(user, role);

        return result;
    }

    public Task<User> GetUserByEmail(User user) => _signInManager.UserManager.FindByEmailAsync(user.Email);
    
    public Task<IdentityResult> AddRole(Role role)
    {
        return _roleManager.CreateAsync(role);
    }
}
