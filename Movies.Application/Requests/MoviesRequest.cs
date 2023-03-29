using Movies.Core.Common;
using Movies.Core.Enums;

namespace Movies.Application.Requests;

public class MoviesRequest : PaginationRequest
{
    public string Name { get; set; }
    public string Synopsis { get; set; }
    public MovieCategory? CategoryId { get; set; }
    public int? ReleaseYear { get; set; }
    public short? Rating { get; set; }
}
