using Movies.Core.Enums;

namespace Movies.Application.Responses.Movies;

public class MoviesResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ReleaseYear { get; set; }
    public string Synopsis { get; set; }
    public byte[] Image { get; set; }
    public MovieCategory CategoryId { get; set; }
    public double Rate { get; set; }
}
