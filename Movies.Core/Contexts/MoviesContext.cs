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
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("ConnectionString"));
    }
}