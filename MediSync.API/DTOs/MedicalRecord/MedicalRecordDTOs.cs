using System.ComponentModel.DataAnnotations;

namespace MediSync.API.DTOs.MedicalRecord;

public class CreateMedicalRecordDTO
{
    [Required(ErrorMessage = "Patient ID is required")]
    public int PatientId { get; set; }

    [Required(ErrorMessage = "Diagnosis is required")]
    [StringLength(500, MinimumLength = 2)]
    public string Diagnosis { get; set; } = string.Empty;

    [Required(ErrorMessage = "Prescription is required")]
    public string Prescription { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    [Required(ErrorMessage = "Visit date is required")]
    public DateTime VisitDate { get; set; }
}

public class UpdateMedicalRecordDTO
{
    public string? Diagnosis { get; set; }
    public string? Prescription { get; set; }
    public string? Notes { get; set; }
    public DateTime? VisitDate { get; set; }
}

public class MedicalRecordResponseDTO
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;
    public string Prescription { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime VisitDate { get; set; }
    public DateTime CreatedAt { get; set; }
}