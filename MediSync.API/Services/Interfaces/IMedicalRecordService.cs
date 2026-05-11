using MediSync.API.DTOs.MedicalRecord;

namespace MediSync.API.Services.Interfaces;

public interface IMedicalRecordService
{
    Task<List<MedicalRecordResponseDTO>> GetAllAsync();
    Task<List<MedicalRecordResponseDTO>> GetByPatientIdAsync(int patientId);
    Task<MedicalRecordResponseDTO> GetByIdAsync(int id);
    Task<MedicalRecordResponseDTO> CreateAsync(CreateMedicalRecordDTO dto);
    Task<MedicalRecordResponseDTO> UpdateAsync(int id, UpdateMedicalRecordDTO dto);
    Task<bool> DeleteAsync(int id);
}