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

        [HttpPost]
        public async Task<ActionResult> AddUser(AddUserRequest request)
        {
            request.Role = UserRoles.User;
            await _userServices.AddUser(request);
            return Ok();
        }

        [HttpPost("Admin")]
        public async Task<ActionResult> AddAdmin(AddUserRequest request)
        {
            request.Role = UserRoles.Admin;
            await _userServices.AddUser(request);
            return Ok();
        }
    }
}
