using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Assignments.Request;
using AssetManagement.Application.Models.DTOs.ReturnRequests.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.API.Controllers
{
    [Route("api/v1/returnRequest")]
    [ApiController]
    public class ReturnRequestController : ControllerBase
    {
        private readonly IReturnRequestServiceAsync _returnRequestServicesAsync;

        public ReturnRequestController(IReturnRequestServiceAsync returnRequestServicesAsync)
        {
            _returnRequestServicesAsync = returnRequestServicesAsync;
        }

        [HttpPost]
        public async Task<IActionResult> AddAssignment([FromBody] AddReturnRequestDto request)
        {
            var result = await _returnRequestServicesAsync.AddReturnRequestAsync(request);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
