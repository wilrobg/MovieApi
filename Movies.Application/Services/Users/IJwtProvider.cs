using Movies.Application.Requests.Users;
using Movies.Core.Entities;

namespace Movies.Application.Services.Users;

public interface IJwtProvider
{
    string GenerateToken(User request);
}
