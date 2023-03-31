using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Requests.Users;
using Movies.Application.Responses.Users;
using Movies.Application.Services.Users;

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

        [HttpPost("Login")]
        public async Task<string> Login(LoginRequest request)
        {
            return await _userServices.LoginUser(request);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddUser(AddUserRequest request)
        {
            await _userServices.AddUser(request);
            return Ok();
        }

        [HttpGet("Roles")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<UserRolesResponse> GetRoles()
        {
            return _userServices.GetUserRoles();
        }
    }
}
