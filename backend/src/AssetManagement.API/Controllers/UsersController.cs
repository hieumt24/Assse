using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServiceAsync _userService;

        public UsersController(IUserServiceAsync userService)
        {
            _userService = userService;
        }

        //POST: api/v1/users
        [HttpPost]
        [Route("users")]
        public async Task<IActionResult> Create([FromBody] AddUserRequestDto request)
        {
            var response = await _userService.AddUserAsync(request);
            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
    }
}