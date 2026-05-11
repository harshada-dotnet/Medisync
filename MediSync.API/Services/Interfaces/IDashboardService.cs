using MediSync.API.DTOs.Dashboard;

namespace MediSync.API.Services.Interfaces;

public interface IDashboardService
{
    Task<DashboardStatsDTO> GetStatsAsync();
}