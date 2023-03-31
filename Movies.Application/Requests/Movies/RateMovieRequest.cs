using System.ComponentModel.DataAnnotations;

namespace Movies.Application.Requests.Movies;

public class RateMovieRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    [Range(1, 10)]
    public short? Rating { get; set; }
}
