using System.ComponentModel.DataAnnotations;

namespace MediSync.API.DTOs.Auth;

public class RegisterDTO
{
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 2)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Role is required")]
    [RegularExpression("Admin|Doctor|Patient", ErrorMessage = "Role must be Admin, Doctor, or Patient")]
    public string Role { get; set; } = "Patient";

    public string? Phone { get; set; }
}
