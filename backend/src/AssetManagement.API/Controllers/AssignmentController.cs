using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Assignment.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.API.Controllers
{
    [Route("api/v1/assignment")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentServicesAsync _assignmentServicesAsync;

        public AssignmentController(IAssignmentServicesAsync assignmentServicesAsync)
        {
            _assignmentServicesAsync = assignmentServicesAsync;
        }

        [HttpPost]
        public async Task<IActionResult> AddAssignment([FromBody] AddAssignmentRequestDto request)
        {
            var result = await _assignmentServicesAsync.AddAssignmentAsync(request);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
