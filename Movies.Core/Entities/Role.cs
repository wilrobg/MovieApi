using Microsoft.AspNetCore.Identity;

namespace Movies.Core.Entities;

public class Role : IdentityRole
{
    private Role() { }
    public static Role CreateRole(string name) { return new Role { Name = name }; }
}
