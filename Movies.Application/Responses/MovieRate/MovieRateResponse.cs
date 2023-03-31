using Movies.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Movies.Application.Responses.MovieRate;

public class MovieRateResponse
{
    public int MovieId { get; set; }

    [JsonPropertyName("name")]
    public string MovieName { get; set; }

    [JsonPropertyName("releaseYear")]
    public int MovieReleaseYear { get; set; }

    [JsonPropertyName("synopsis")]
    public string MovieSynopsis { get; set; }

    [JsonPropertyName("categoryId")]
    public MovieCategory MovieCategoryId { get; set; }
    public short Rate { get; set; }
}
