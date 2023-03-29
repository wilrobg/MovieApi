using Movies.Core.Enums;

namespace Movies.Core.Models;

public class MoviesResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ReleaseYear { get; set; }
    public string Synopsis { get; set; }
    public byte[] Image { get; set; }
    public MovieCategory CategoryId { get; set; }
    public short Rating { get; set; }
}
