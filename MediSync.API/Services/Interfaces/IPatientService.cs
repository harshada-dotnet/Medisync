using MediSync.API.DTOs.Patient;

namespace MediSync.API.Services.Interfaces;

public interface IPatientService
{
    Task<List<PatientResponseDTO>> GetAllAsync(string? search);
    Task<PatientResponseDTO> GetByIdAsync(int id);
    Task<PatientResponseDTO> CreateAsync(CreatePatientDTO dto);
    Task<PatientResponseDTO> UpdateAsync(int id, UpdatePatientDTO dto);
    Task<bool> DeleteAsync(int id);
}