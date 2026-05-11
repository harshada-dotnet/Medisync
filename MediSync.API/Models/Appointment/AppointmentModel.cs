using MediSync.API.Models.Patient;
using MediSync.API.Models.Doctor;

namespace MediSync.API.Models.Appointment;

public class Appointment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string TimeSlot { get; set; } = string.Empty;
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Patient.Patient Patient { get; set; } = null!;
    public Doctor.Doctor Doctor { get; set; } = null!;
}

public enum AppointmentStatus
{
    Pending,
    Confirmed,
    Cancelled,
    Completed
}