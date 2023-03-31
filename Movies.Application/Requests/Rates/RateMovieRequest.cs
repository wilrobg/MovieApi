using System.ComponentModel.DataAnnotations;

namespace Movies.Application.Requests.Rates;

public class RateMovieRequest
{
    [Required]
    public int MovieId { get; set; }

    [Required]
    [Range(1, 10)]
    public short? Rate { get; set; }
}
