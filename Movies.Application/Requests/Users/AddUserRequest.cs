using Movies.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Movies.Application.Requests.Users;

public class AddUserRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$")]
    public string Password { get; set; }

    [JsonIgnore]
    public UserRoles Role { get; set; }
}
