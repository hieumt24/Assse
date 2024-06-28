using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Application.Models.DTOs.Assignments.Request;
using AssetManagement.Application.Models.DTOs.Assignments.Requests;
using AssetManagement.Application.Models.DTOs.ReturnRequests.Request;
using AssetManagement.Application.Models.Filters;
using AssetManagement.Domain.Enums;
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

        [HttpPost]
        [Route("filter-user-assigned")]
        public async Task<IActionResult> GetAssignmentsForUser([FromBody] FilterAssignmentForUser filterAssignmentForUser)
        {
            string route = Request.Path.Value;
            var result = await _assignmentServicesAsync.GetAssignmentsOfUser(filterAssignmentForUser.pagination, filterAssignmentForUser.userId, filterAssignmentForUser.search, filterAssignmentForUser.assignmentState, filterAssignmentForUser.assignedDate, filterAssignmentForUser.orderBy, filterAssignmentForUser.isDescending, route);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GerAssignmentById(Guid assignmentId)
        {
            var result = await _assignmentServicesAsync.GetAssignmentByIdAsync(assignmentId);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeAssignmentStatus(ChangeStateAssignmentDto request)
        {
            var result = await _assignmentServicesAsync.ChangeAssignmentStateAsync(request);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}