using Microsoft.EntityFrameworkCore;
using MediSync.API.Data;
using MediSync.API.DTOs.MedicalRecord;
using MediSync.API.Models.Patient;
using MediSync.API.Services.Interfaces;

namespace MediSync.API.Services;

public class MedicalRecordService : IMedicalRecordService
{
    private readonly ApplicationDbContext _context;

    public MedicalRecordService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MedicalRecordResponseDTO>> GetAllAsync()
    {
        return await _context.MedicalRecords
            .Include(m => m.Patient)
            .OrderByDescending(m => m.VisitDate)
            .Select(m => MapToDTO(m))
            .ToListAsync();
    }

    public async Task<List<MedicalRecordResponseDTO>> GetByPatientIdAsync(int patientId)
    {
        // Check patient exists
        var patientExists = await _context.Patients
            .AnyAsync(p => p.Id == patientId && p.IsActive);

        if (!patientExists)
            throw new KeyNotFoundException($"Patient with ID {patientId} not found.");

        return await _context.MedicalRecords
            .Include(m => m.Patient)
            .Where(m => m.PatientId == patientId)
            .OrderByDescending(m => m.VisitDate)
            .Select(m => MapToDTO(m))
            .ToListAsync();
    }

    public async Task<MedicalRecordResponseDTO> GetByIdAsync(int id)
    {
        var record = await _context.MedicalRecords
            .Include(m => m.Patient)
            .FirstOrDefaultAsync(m => m.Id == id)
            ?? throw new KeyNotFoundException($"Medical record with ID {id} not found.");

        return MapToDTO(record);
    }

    public async Task<MedicalRecordResponseDTO> CreateAsync(CreateMedicalRecordDTO dto)
    {
        // Check patient exists
        var patientExists = await _context.Patients
            .AnyAsync(p => p.Id == dto.PatientId && p.IsActive);

        if (!patientExists)
            throw new KeyNotFoundException($"Patient with ID {dto.PatientId} not found.");

        var record = new MedicalRecord
        {
            PatientId = dto.PatientId,
            Diagnosis = dto.Diagnosis.Trim(),
            Prescription = dto.Prescription.Trim(),
            Notes = dto.Notes.Trim(),
            VisitDate = dto.VisitDate
        };

        _context.MedicalRecords.Add(record);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(record.Id);
    }

    public async Task<MedicalRecordResponseDTO> UpdateAsync(int id, UpdateMedicalRecordDTO dto)
    {
        var record = await _context.MedicalRecords
            .FirstOrDefaultAsync(m => m.Id == id)
            ?? throw new KeyNotFoundException($"Medical record with ID {id} not found.");

        if (dto.Diagnosis != null) record.Diagnosis = dto.Diagnosis.Trim();
        if (dto.Prescription != null) record.Prescription = dto.Prescription.Trim();
        if (dto.Notes != null) record.Notes = dto.Notes.Trim();
        if (dto.VisitDate.HasValue) record.VisitDate = dto.VisitDate.Value;

        await _context.SaveChangesAsync();
        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var record = await _context.MedicalRecords
            .FirstOrDefaultAsync(m => m.Id == id)
            ?? throw new KeyNotFoundException($"Medical record with ID {id} not found.");

        _context.MedicalRecords.Remove(record);
        await _context.SaveChangesAsync();
        return true;
    }

    private static MedicalRecordResponseDTO MapToDTO(MedicalRecord m) => new()
    {
        Id = m.Id,
        PatientId = m.PatientId,
        PatientName = m.Patient?.FullName ?? string.Empty,
        Diagnosis = m.Diagnosis,
        Prescription = m.Prescription,
        Notes = m.Notes,
        VisitDate = m.VisitDate,
        CreatedAt = m.CreatedAt
    };
}