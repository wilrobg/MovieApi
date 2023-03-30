using Bogus;
using Microsoft.EntityFrameworkCore;
using Movies.Core.Abstractions;
using Movies.Core.Contexts;
using Movies.Core.Entities;
using Movies.Core.Enums;
using Movies.Core.Models;

namespace Movies.Core.Seeders;

public static class MoviesSeeder
{
    public static async Task Seed(this MoviesContext context)
    {
        if (!await context.Movies.AnyAsync())
        {
            var movies = new Faker<Movie>()
                .RuleFor(x => x.Name, f => f.Random.Words(3))
                .RuleFor(x => x.ReleaseYear, f => f.Random.Number(1950, DateTime.Now.Year))
                .RuleFor(x => x.Synopsis, f => f.Lorem.Paragraph(5))
                .RuleFor(x => x.CategoryId, f => f.PickRandom<MovieCategory>())
                .RuleFor(x => x.Rating, f => (short)f.Random.Number(1, 10))
                .Generate(500);

            await context.Movies.AddRangeAsync(movies);

            await context.SaveChangesAsync();
        }
    }
}
