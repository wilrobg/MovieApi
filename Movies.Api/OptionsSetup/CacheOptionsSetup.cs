using Microsoft.Extensions.Options;
using Movies.Infrastructure.Cache;

namespace Movies.Api.OptionsSetup;

public class CacheOptionsSetup : IConfigureOptions<CacheOptions>
{
    const string SectionName = "Cache";
    private readonly IConfiguration _configuration;

    public CacheOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(CacheOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
