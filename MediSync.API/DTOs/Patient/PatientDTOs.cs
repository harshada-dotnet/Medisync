using System.ComponentModel.DataAnnotations;

namespace MediSync.API.DTOs.Patient;

public class CreatePatientDTO
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public string Gender { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string Phone { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string BloodGroup { get; set; } = string.Empty;
}

public class UpdatePatientDTO : CreatePatientDTO { }

public class PatientResponseDTO
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string BloodGroup { get; set; } = string.Empty;
    public DateTime RegisteredAt { get; set; }
}
