using Movies.Api.Extensions;
using Movies.Application;
using Movies.Application.Profiles;
using Movies.Core;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine(builder.Configuration.GetConnectionString("ConnectionString") ?? "Something");

builder.Services.AddAutoMapper(typeof(MoviesProfile));

builder.Services.AddApplication();

builder.Services.AddDbContextSetup();

await builder.Services.DatabaseSeeder();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
var environment = app.Environment.IsDevelopment();
if (environment)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseGlobalExceptionHandler(environment);

app.UseAuthorization();

app.MapControllers();

app.Run();