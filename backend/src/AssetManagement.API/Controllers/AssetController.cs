using AssetManagement.API.CustomActionFilters;
using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Models.DTOs.Assets.Requests;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.API.Controllers
{
    [Route("api/v1/assets")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        public readonly IAssetServiceAsync _assetService;

        public AssetController(IAssetServiceAsync assetService)
        {
            _assetService = assetService;
        }

        [HttpPost]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(Guid id)
        {
            var result = await _assetService.DeleteAssetAsync(id);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetById(Guid id)
        {
            var result = await _assetService.GetAssetByIdAsync(id);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
