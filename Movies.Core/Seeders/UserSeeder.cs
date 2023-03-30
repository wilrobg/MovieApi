using Movies.Core.Abstractions;
using Movies.Core.Entities;
using Movies.Core.Repositories;

namespace Movies.Core.Seeders;

public static class UserSeeder
{
    public static async Task Seed(this UserRepository userRepository)
    
    {
        var user = User.CreateUser("user@movies.com");

        var dbUser = await userRepository.GetUserByEmail(user);
        if(dbUser is null)
        {
            var userRole = Role.CreateRole(Enums.UserRoles.User.GetEnumDescription());
            _ = await userRepository.AddRole(userRole);

            var adminRole = Role.CreateRole(Enums.UserRoles.Admin.GetEnumDescription());
            _ = await userRepository.AddRole(adminRole);

            _ = await userRepository.CreateUser(user, "Test1234+", userRole.Name);

            var admin = User.CreateUser("admin@movies.com");
            _ = await userRepository.CreateUser(admin, "Test1234+", adminRole.Name);
        }
    }
}
