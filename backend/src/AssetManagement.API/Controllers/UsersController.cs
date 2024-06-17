using AssetManagement.API.CustomActionFilters;
using AssetManagement.Application.Filter;
using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Models.DTOs.Users;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AssetManagement.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServiceAsync _userService;
        private readonly IValidator<AddUserRequestDto> _validator;

        public UsersController(IUserServiceAsync userService, IValidator<AddUserRequestDto> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        [HttpPost]
        [Route("users")]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddUserRequestDto request)
        {
            var response = await _userService.AddUserAsync(request);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] PaginationFilter filter, [FromQuery] string? search, [FromQuery] string? orderBy, [FromQuery] bool isDescending = false, [FromQuery] EnumLocation? adminLocation = EnumLocation.HaNoi)
        {
            string route = Request.Path.Value;
            var response = await _userService.GetAllUsersAsync(filter, search, orderBy, isDescending, adminLocation, route);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("users/{userId:guid}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var response = await _userService.GetUserByIdAsync(userId);
            if (!response.Succeeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] EditUserRequestDto request)

        {
            if (request == null)
            {
                return BadRequest(new Response<UserDto> { Succeeded = false, Errors = { "Invalid request data" } });
            }

            var result = await _userService.EditUserAsync(request);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        [Route("disable/{id}")]
        public async Task<IActionResult> DisableUser(Guid id)
        {
            var response = await _userService.DisableUserAsync(id);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(Guid userId)
        {
            var response = await _userService.ResetPasswordAsync(userId);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet]
        [Route("users/staffCode/{staffCode}")]
        public async Task<IActionResult> GetUserByStaffCode(string staffCode)
        {
            var response = await _userService.GetUserByStaffCodeAsync(staffCode);
            if (!response.Succeeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}