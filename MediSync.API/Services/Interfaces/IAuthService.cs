using MediSync.API.DTOs.Auth;

namespace MediSync.API.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDTO> RegisterAsync(RegisterDTO dto);
    Task<AuthResponseDTO> LoginAsync(LoginDTO dto);
}
