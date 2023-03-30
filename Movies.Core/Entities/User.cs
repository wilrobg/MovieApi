using Microsoft.AspNetCore.Identity;

namespace Movies.Core.Entities;

public class User : IdentityUser
{
    private User() { }

    public static User CreateUser(string email)
    {
        return new User
        {
            Email = email,
            UserName= email
        };
    }
}
