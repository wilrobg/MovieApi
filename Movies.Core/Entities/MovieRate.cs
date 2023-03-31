using Movies.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Entities;

public class MovieRate
{
    [Key]
    public int Id { get; set; }
    public short Rate { get; set; }
    [Required]
    public DateTime UpdatedDate { get; set; }

    public int MovieId { get; set; }
    public virtual Movie Movie { get; set; }

    public string UserId { get; set; }
    public virtual User User { get; set; }
}
