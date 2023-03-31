using CiudadGambito.Api.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Movies.Api.HttpContextAccessor;
using Movies.Api.OptionsSetup;
using Movies.Application;
using Movies.Application.Profiles;
using Movies.Core;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MoviesProfile));

builder.Services.AddSingleton<IUserHttpContextAccesor, UserHttpContextAccesor>();

builder.Services.AddApplication();

builder.Services.AddDbContextSetup();

await builder.Services.DatabaseSeeder();

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(new ApiExceptionFilterAttribute()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

});

builder.Services.AddAuthentication(cfg =>
{
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer();


builder.Services.AddAuthorization(options => options.AddPolicy("Roles", policy => policy.RequireClaim(ClaimTypes.Role, "Admin")));

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

var app = builder.Build();

// Configure the HTTP request pipeline.
var environment = app.Environment.IsDevelopment();
if (environment)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthentication();

app.UseAuthorization();

app.Run();
