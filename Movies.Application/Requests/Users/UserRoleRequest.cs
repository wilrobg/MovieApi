using Movies.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Movies.Application.Requests.Users;

public class UserRoleRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [EnumDataType(typeof(UserRoles))]
    public UserRoles Role { get; set; }
}
