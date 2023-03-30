using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Requests.Users;
using Movies.Application.Services.Users;
using Movies.Core.Enums;

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
        public async Task<ActionResult> AddUser(AddUserRequest request)
        {
            request.Role = UserRoles.User;
            await _userServices.AddUser(request);
            return Ok();
        }

        [HttpPost("Admin")]
        [Authorize]
        public async Task<ActionResult> AddAdmin(AddUserRequest request)
        {
            request.Role = UserRoles.Admin;
            await _userServices.AddUser(request);
            return Ok();
        }
    }
}
