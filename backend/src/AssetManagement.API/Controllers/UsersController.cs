using AssetManagement.API.CustomActionFilters;
using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
            //if (!ModelState.IsValid)
            //{
            //    //var res = new Response<AddUserRequestDto>("Invalid input");
            //    return BadRequest(ModelState);
            //}
            var response = await _userService.AddUserAsync(request);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}