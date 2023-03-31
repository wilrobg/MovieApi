using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Movies.Application.Exceptions;
using Movies.Application.Requests.Users;
using Movies.Application.Responses.Users;
using Movies.Core.Abstractions;
using Movies.Core.Entities;
using Movies.Core.Enums;
using Movies.Core.Repositories;
using System.Net;

namespace Movies.Application.Services.Users;


//Create an endpoint to remove a movie rating for the authenticated user.
//Create an endpoint to return all movies ratings for the authenticated user
//Cache the response of the list movie endpoint

public class UserServices : IUserServices
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserServices> _logger;

    public UserServices(
        IJwtProvider jwtProvider, 
        IUserRepository userRepository, 
        ILogger<UserServices> logger)
    {
        _jwtProvider = jwtProvider;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task AddUser(AddUserRequest request)
    {
        var user = User.CreateUser(request.Email);

        var role = request.Role.GetEnumDescription();
        var result = await _userRepository.CreateUser(user, request.Password, role);

        if (!result.Succeeded) LogIdentityErrors(result);
    }

    public IEnumerable<UserRolesResponse> GetUserRoles()
    {
        var movieCategories = Enum.GetValues<UserRoles>()
           .Select(m => new UserRolesResponse(m, m.GetEnumDescription()));

        return movieCategories;
    }

    public async Task<string> LoginUser(LoginRequest loginRequest)
    {
        var user = await _userRepository.GetUserByEmail(loginRequest.Email);

        if (user is null) throw new MoviesException(HttpStatusCode.Unauthorized);

        var isValidPassword = await _userRepository.CheckPassword(user, loginRequest.Password);

        if (!isValidPassword) throw new MoviesException(HttpStatusCode.Unauthorized);

        var roles = await _userRepository.GetUserRoles(user);

        return _jwtProvider.GenerateToken(user, roles);
    }

    public async Task AddUserRole(UserRoleRequest request)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);

        if (user is null) throw new MoviesException(HttpStatusCode.Unauthorized);

        var userRoles = await _userRepository.GetUserRoles(user);

        var role = request.Role.GetEnumDescription();

        if(userRoles.Contains(role)) throw new MoviesException(HttpStatusCode.Conflict, "User already have the role.");

        var result = await _userRepository.AddUserRole(user, role);

        if (!result.Succeeded) LogIdentityErrors(result);
    }

    public async Task RemoveUserRole(UserRoleRequest request)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);

        if (user is null) throw new MoviesException(HttpStatusCode.Unauthorized);

        var userRoles = await _userRepository.GetUserRoles(user);

        var role = request.Role.GetEnumDescription();

        if (!userRoles.Contains(role)) throw new MoviesException(HttpStatusCode.Conflict, "User not have the role assigned.");

        var result = await _userRepository.RemoveFromRole(user, role);

        if (!result.Succeeded) LogIdentityErrors(result);
    }

    public void LogIdentityErrors(IdentityResult result)
    {
        var errors = result.Errors.ToDictionary(e => e.Code, e => e.Description);
        var message = string.Join(Environment.NewLine, errors);
        _logger.LogError("Identity errors: {0}", message);
        throw new MoviesException(HttpStatusCode.Conflict, "Error adding rol");
    }
}
