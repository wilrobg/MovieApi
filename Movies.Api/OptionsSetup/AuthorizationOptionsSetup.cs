using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Movies.Api.OptionsSetup;

public class AuthorizationOptionsSetup : IConfigureNamedOptions<AuthorizationOptions>
{
    public void Configure(string name, AuthorizationOptions options)
    {
        options.AddPolicy("Roles", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    }

    public void Configure(AuthorizationOptions options)
    {
        throw new NotImplementedException();
    }
}
