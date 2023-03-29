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
    public MovieCategory Category { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }
}
