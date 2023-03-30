using Microsoft.Extensions.Logging;
using Movies.Application.Exceptions;
using Movies.Application.Requests.Users;
using Movies.Core.Abstractions;
using Movies.Core.Entities;
using Movies.Core.Repositories;
using System.Net;

namespace Movies.Application.Services.Users;

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

        if (!result.Succeeded)
        {
            var errors = result.Errors.ToDictionary(e => e.Code, e => e.Description);
            var message = string.Join(Environment.NewLine, errors);
            _logger.LogError("Identity errors: {0}", message);
            throw new MoviesException(HttpStatusCode.Conflict, "Error adding user");
        }
    }

    public async Task<string> LoginUser(LoginRequest loginRequest)
    {
        var user = await _userRepository.GetUserByEmail(loginRequest.Email);

        if (user is null) throw new MoviesException(HttpStatusCode.Unauthorized);

        var isValidPassword = await _userRepository.CheckPassword(user, loginRequest.Password);

        if (!isValidPassword) throw new MoviesException(HttpStatusCode.Unauthorized);

        return _jwtProvider.GenerateToken(user);
    }
}
