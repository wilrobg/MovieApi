using Movies.Core.Entities;

namespace Movies.Infrastructure.Authentication;

public interface IJwtProvider
{
    string GenerateToken(User request, IList<string> roles);
}
