using AssetManagement.API.CustomActionFilters;
using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Models.DTOs.Assets.Requests;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        public readonly IAssetServiceAsync _assetService;

        public AssetController(IAssetServiceAsync assetService)
        {
            _assetService = assetService;
        }

        [HttpPost]
        [Route("assets")]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddAssetRequestDto request)
        {
            var response = await _assetService.AddAssetAsync(request);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
