using System.ComponentModel.DataAnnotations;

namespace MediSync.API.DTOs.Doctor;

public class CreateDoctorDTO
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public string Specialization { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string Phone { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Range(0, 60)]
    public int ExperienceYears { get; set; }

    public string Qualification { get; set; } = string.Empty;
}

public class UpdateDoctorDTO : CreateDoctorDTO { }

public class DoctorResponseDTO
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
    public string Qualification { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
}
