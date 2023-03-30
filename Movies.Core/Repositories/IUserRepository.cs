using Microsoft.AspNetCore.Identity;
using Movies.Core.Entities;

namespace Movies.Core.Repositories;

public class IUserRepository
{
	private readonly SignInManager<User> _signInManager;

    public IUserRepository(SignInManager<User> signInManager)
	{
        _signInManager = signInManager;
    }
}
