using System.ComponentModel.DataAnnotations;

namespace Movies.Application.Requests.Users;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
