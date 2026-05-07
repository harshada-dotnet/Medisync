using MediSync.API.Data;
using MediSync.API.DTOs.Auth;
using MediSync.API.Models.Auth;
using MediSync.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediSync.API.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly IConfiguration _config;

    public AuthService(ApplicationDbContext context, IJwtService jwtService, IConfiguration config)
    {
        _context    = context;
        _jwtService = jwtService;
        _config     = config;
    }

    public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO dto)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == dto.Email.ToLower());

        if (existingUser != null)
            throw new InvalidOperationException("Email already registered.");

        var user = new ApplicationUser
        {
            FullName     = dto.FullName.Trim(),
            Email        = dto.Email.ToLower().Trim(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role         = dto.Role,
            Phone        = dto.Phone
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var token      = _jwtService.GenerateToken(user);
        var expiryDays = int.Parse(_config["JwtSettings:ExpiryInDays"]!);

        return new AuthResponseDTO
        {
            Token     = token,
            FullName  = user.FullName,
            Email     = user.Email,
            Role      = user.Role,
            ExpiresAt = DateTime.UtcNow.AddDays(expiryDays)
        };
    }

    public async Task<AuthResponseDTO> LoginAsync(LoginDTO dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == dto.Email.ToLower());

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password.");

        if (!user.IsActive)
            throw new UnauthorizedAccessException("Account is deactivated.");

        var token      = _jwtService.GenerateToken(user);
        var expiryDays = int.Parse(_config["JwtSettings:ExpiryInDays"]!);

        return new AuthResponseDTO
        {
            Token     = token,
            FullName  = user.FullName,
            Email     = user.Email,
            Role      = user.Role,
            ExpiresAt = DateTime.UtcNow.AddDays(expiryDays)
        };
    }
}
