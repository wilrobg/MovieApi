using Movies.Core.Entities;
using Movies.Core.Enums;
using Movies.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Movies.Application.Responses.Movies;

public class MoviesResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ReleaseYear { get; set; }
    public string Synopsis { get; set; }
    public MovieCategory CategoryId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public double Rate => MovieRateSum / (MovieRateCount == 0 ? 1d : MovieRateCount);

    [JsonIgnore]
    public short MovieRateSum { get; set; }
    [JsonIgnore]
    public int MovieRateCount { get; set; }
}
