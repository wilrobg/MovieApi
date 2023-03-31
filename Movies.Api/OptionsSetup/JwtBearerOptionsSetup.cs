using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Movies.Infrastructure.Authentication;
using System.Security.Claims;
using System.Text;
using static IdentityModel.ClaimComparer;


namespace Movies.Api.OptionsSetup;

public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptions;

    public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public void Configure(string name, JwtBearerOptions options)
    {
        options.TokenValidationParameters = GetTokenValidationParameters();
    }

    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = GetTokenValidationParameters();
    }

    private TokenValidationParameters GetTokenValidationParameters()
    {
        return new()
        {
            RoleClaimType = ClaimTypes.Role,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
        };
    }
}
