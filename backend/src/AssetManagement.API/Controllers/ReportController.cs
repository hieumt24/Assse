using AssetManagement.Application.Interfaces.Services;
using AssetManagement.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.API.Controllers
{
    [Route("api/v1/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportServices _reportServices;

        public ReportController(IReportServices reportServices)
        {
            _reportServices = reportServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetReport(EnumLocation location)
        {
            var result = await _reportServices.GetReportAsync(location);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}