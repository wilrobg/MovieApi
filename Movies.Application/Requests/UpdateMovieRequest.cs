
using Movies.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Movies.Application.Requests;

public class UpdateMovieRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    [Range(1900, 2023)]
    public int? ReleaseYear { get; set; }

    [Required]
    public string Synopsis { get; set; }

    [Required]
    [EnumDataType(typeof(MovieCategory))]
    public MovieCategory? CategoryId { get; set; }

    [Required]
    [Range(1, 10)]
    public short? Rating { get; set; }
}
