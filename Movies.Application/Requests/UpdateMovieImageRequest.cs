using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Movies.Application.Requests;

public class UpdateMovieImageRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public IFormFile Image { get; set; }
}
