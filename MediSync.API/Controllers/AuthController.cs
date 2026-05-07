using Microsoft.AspNetCore.Mvc;
using MediSync.API.DTOs.Auth;
using MediSync.API.Helpers;
using MediSync.API.Services.Interfaces;

namespace MediSync.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>Register a new user (Admin / Doctor / Patient)</summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<string>.Fail("Validation failed."));

        try
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(ApiResponse<AuthResponseDTO>.Ok(result, "Registration successful."));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ApiResponse<string>.Fail(ex.Message));
        }
    }

    /// <summary>Login with email and password — returns JWT token</summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<string>.Fail("Validation failed."));

        try
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(ApiResponse<AuthResponseDTO>.Ok(result, "Login successful."));
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ApiResponse<string>.Fail(ex.Message));
        }
    }
}
