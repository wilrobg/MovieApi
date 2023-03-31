using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Requests.Users;
using Movies.Application.Responses.Users;
using Movies.Application.Services.Users;
using Swashbuckle.AspNetCore.Annotations;

namespace Movies.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [SwaggerOperation(Description = "Only for Admins. Enter a valid email and password.")]
        [HttpPost("Login")]
        public async Task<string> Login(LoginRequest request)
        {
            return await _userServices.LoginUser(request);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Add user endpoint", Description = "Only for Admins. Enter a valid email and password.")]
        public async Task<ActionResult> AddUser(AddUserRequest request)
        {
            await _userServices.AddUser(request);
            return Ok();
        }

        [HttpPost("Role")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Add role to a user", Description = "Only for Admins. Get rolesId from Users/Roles")]
        public async Task<ActionResult> AddUserRole(UserRoleRequest request)
        {
            await _userServices.AddUserRole(request);
            return Ok();
        }

        [HttpDelete("Role")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Remove role to a user", Description = "Only for Admins. Get rolesId from Users/Roles")]
        public async Task<ActionResult> RemoveUserRole(UserRoleRequest request)
        {
            await _userServices.RemoveUserRole(request);
            return Ok();
        }

        [HttpGet("Roles")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Get Roles", Description = "Only for Admins. Get roleId")]
        public IEnumerable<UserRolesResponse> GetRoles()
        {
            return _userServices.GetUserRoles();
        }
    }
}
