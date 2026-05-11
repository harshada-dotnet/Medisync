using MediSync.API.DTOs.Appointment;

namespace MediSync.API.Services.Interfaces;

public interface IAppointmentService
{
    Task<List<AppointmentResponseDTO>> GetAllAsync();
    Task<List<AppointmentResponseDTO>> GetByPatientIdAsync(int patientId);
    Task<List<AppointmentResponseDTO>> GetByDoctorIdAsync(int doctorId);
    Task<AppointmentResponseDTO> GetByIdAsync(int id);
    Task<AppointmentResponseDTO> CreateAsync(CreateAppointmentDTO dto);
    Task<AppointmentResponseDTO> UpdateStatusAsync(int id, UpdateAppointmentStatusDTO dto);
    Task<bool> DeleteAsync(int id);
}