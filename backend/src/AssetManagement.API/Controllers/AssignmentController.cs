using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Assignments.Request;
using AssetManagement.Application.Models.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.API.Controllers
{
    [Route("api/v1/assignments")]
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

        [HttpPost]
        [Route("filter-assignments")]
        public async Task<IActionResult> FilterAssignment([FromBody] AssignmentFilter filter)
        {
            string route = Request.Path.Value;
            var response = await _assignmentServicesAsync.GetAllAssignmentsAsync(filter.pagination, filter.search, filter.assignmentState, filter.assignedDate, filter.adminLocation, filter.orderBy, filter.isDescending, route);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}