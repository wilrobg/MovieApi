using Movies.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Entities;

public class MovieRate
{
    [Key]
    public int Id { get; set; }
    public short Rate { get; set; }
    [Required]
    public DateTime UpdatedDate { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }

    public static MovieRate Create(Movie movie, string userId, short rate)
    {
        return new MovieRate
        {
            Movie = movie,
            UserId = userId,
            Rate = rate
        };
    }
}
