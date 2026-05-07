using MediSync.API.Models.Auth;

namespace MediSync.API.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(ApplicationUser user);
}
