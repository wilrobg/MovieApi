using Movies.Core.Entities;
using Movies.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Models;

public class Movie
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public int ReleaseYear { get; set; }

    [Required]
    public string Synopsis { get; set; }

    public byte[] Image { get; set; }

    [Required]
    public MovieCategory CategoryId { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }
    [Required]
    public string CreatedBy { get; set; }

    public virtual ICollection<MovieRate> MovieRates { get; set;}

    public static Movie Create(string name, int releaseYear, string synopsis, MovieCategory categoryId, string createdBy)
    {
        return new Movie()
        {
            Name= name,
            ReleaseYear= releaseYear,
            Synopsis= synopsis,
            CategoryId= categoryId,
            CreatedBy= createdBy
        };
    }
}
