using System.ComponentModel.DataAnnotations;

namespace Movies.Application.Requests.Rates;

public class RateMovieRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    [Range(1, 10)]
    public short? Rate { get; set; }
}
