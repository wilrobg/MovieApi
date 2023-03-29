using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Movies.Core.Enums;
using Movies.Core.Models;

namespace Movies.Core.Contexts;

public class MoviesContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public MoviesContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

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

    public DbSet<Movie> Movies { get; set; }

    public async Task AddSeeder()
    {
        Database.Migrate();

        if (!await Movies.AnyAsync())
        {
            var movies = new Faker<Movie>()
                .RuleFor(x => x.Name, f => f.Random.Words(3))
                .RuleFor(x => x.ReleaseYear, f => f.Random.Number(1950, DateTime.Now.Year))
                .RuleFor(x => x.Synopsis, f => f.Lorem.Paragraph(5))
                .RuleFor(x => x.Category, f => f.PickRandom<MovieCategory>())
                .Generate(500);

            await Movies.AddRangeAsync(movies);

            await SaveChangesAsync();
        }
    }
}