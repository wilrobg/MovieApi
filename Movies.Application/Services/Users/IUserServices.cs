using Movies.Application.Requests.Users;
using Movies.Application.Responses.Users;

namespace Movies.Application.Services.Users;

public interface IUserServices
{
    Task AddUser(AddUserRequest login);
    Task<string> LoginUser(LoginRequest login);
    IEnumerable<UserRolesResponse> GetUserRoles();
}
