using Bogus;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Movies.Core.Enums;
using Movies.Core.Models;
using Movies.Core.Entities;

namespace Movies.Core.Contexts;

public class MoviesContext : IdentityDbContext<IdentityUser>
{
    protected readonly IConfiguration Configuration;

    public MoviesContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movie>(m =>
        {
            m.Property(x => x.CreatedDate).HasDefaultValueSql("NOW()");
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("ConnectionString"));
    }

    public async Task AddSeeder()
    {
        if (!await Movies.AnyAsync())
        {
            var movies = new Faker<Movie>()
                .RuleFor(x => x.Name, f => f.Random.Words(3))
                .RuleFor(x => x.ReleaseYear, f => f.Random.Number(1950, DateTime.Now.Year))
                .RuleFor(x => x.Synopsis, f => f.Lorem.Paragraph(5))
                .RuleFor(x => x.CategoryId, f => f.PickRandom<MovieCategory>())
                .RuleFor(x => x.Rating, f => (short)f.Random.Number(1,10))
                .Generate(500);

            await Movies.AddRangeAsync(movies);

            await SaveChangesAsync();
        }
    }
}