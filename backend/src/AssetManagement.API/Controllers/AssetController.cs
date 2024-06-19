using AssetManagement.API.CustomActionFilters;
using AssetManagement.Application.Filter;
using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Assets.Requests;
using AssetManagement.Application.Models.Filters;
using AssetManagement.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] AddAssetRequestDto request)
        {
            var response = await _assetService.AddAssetAsync(request);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("filter-assets")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsset([FromBody] AssetFilter assetFilter)
        {
            string route = Request.Path.Value;
            var response = await _assetService.GetAllAseets(assetFilter.pagination, assetFilter.search, assetFilter.categoryId, assetFilter.assetStateType, assetFilter.enumLocation, assetFilter.orderBy, assetFilter.isDescending, route);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}