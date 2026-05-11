using MediSync.API.Models.Appointment;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MediSync.API.DTOs.Appointment;

public class CreateAppointmentDTO
{
    [Required]
    public int PatientId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    public DateTime AppointmentDate { get; set; }

    [Required]
    public string TimeSlot { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;
}

public class UpdateAppointmentStatusDTO
{
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AppointmentStatus Status { get; set; }
}

public class AppointmentResponseDTO
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public string DoctorSpecialization { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public string TimeSlot { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}


