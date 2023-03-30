using Movies.Application.Requests.Users;

namespace Movies.Application.Services.Users;

public interface IUserServices
{
    Task AddUser(AddUserRequest login);
    Task<string> LoginUser(LoginRequest login);
}
