using Bogus;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Movies.Core.Enums;
using Movies.Core.Models;
using Movies.Core.Entities;
using Movies.Core.Abstractions;

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
            m.HasIndex(x => x.UserId);
        });

        modelBuilder.Entity<MovieRate>(m =>
        {
            m.Property(x => x.UpdatedDate).HasDefaultValueSql("NOW()");

            m.HasOne(x => x.Movie)
            .WithMany(x => x.MovieRates)
            .HasForeignKey(x => x.MovieId);

            m.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

            m.HasIndex(x => new { x.MovieId, x.UserId }).IsUnique();
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("ConnectionString"));
    }
}