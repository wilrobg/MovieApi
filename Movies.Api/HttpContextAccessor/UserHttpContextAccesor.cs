﻿using Movies.Infrastructure.HttpContextAccessor;
using System.Security.Claims;

namespace Movies.Api.HttpContextAccessor;

public class UserHttpContextAccesor : IUserHttpContextAccesor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserHttpContextAccesor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
    public List<string> Roles => _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
}
