namespace MediSync.API.Models.Patient;

public class MedicalRecord
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public string Prescription { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime VisitDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Patient Patient { get; set; } = null!;
}