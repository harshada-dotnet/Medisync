using MediSync.API.DTOs.Doctor;

namespace MediSync.API.Services.Interfaces;

public interface IDoctorService
{
    Task<List<DoctorResponseDTO>> GetAllAsync(string? search);
    Task<DoctorResponseDTO> GetByIdAsync(int id);
    Task<DoctorResponseDTO> CreateAsync(CreateDoctorDTO dto);
    Task<DoctorResponseDTO> UpdateAsync(int id, UpdateDoctorDTO dto);
    Task<bool> DeleteAsync(int id);
}