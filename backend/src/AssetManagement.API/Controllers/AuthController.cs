using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountServicecs _accountService;

        public AuthController(IAccountServicecs accountService)
        {
            _accountService = accountService;
        }

        //POST api/v1/auth/login
        [HttpPost]
        [Route("auth/login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest request)
        {
            var response = await _accountService.LoginAsync(request);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        //POST api/v1/auth/change-password
        [HttpPost]
        [Route("auth/change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var response = await _accountService.ChangePasswordAsync(request);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}