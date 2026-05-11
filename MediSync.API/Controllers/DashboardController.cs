using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediSync.API.DTOs.Dashboard;
using MediSync.API.Helpers;
using MediSync.API.Services.Interfaces;

namespace MediSync.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    /// <summary>Get dashboard stats — total patients, doctors, appointments, today's count</summary>
    [HttpGet("stats")]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> GetStats()
    {
        var result = await _dashboardService.GetStatsAsync();
        return Ok(ApiResponse<DashboardStatsDTO>.Ok(result));
    }
}