using Microsoft.EntityFrameworkCore;
using MediSync.API.Data;
using MediSync.API.DTOs.Patient;
using MediSync.API.Models.Patient;
using MediSync.API.Services.Interfaces;

namespace MediSync.API.Services;

public class PatientService : IPatientService
{
    private readonly ApplicationDbContext _context;

    public PatientService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PatientResponseDTO>> GetAllAsync(string? search)
    {
        var query = _context.Patients.Where(p => p.IsActive);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p =>
                p.FullName.Contains(search) ||
                p.Email.Contains(search) ||
                p.Phone.Contains(search));

        return await query
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => MapToDTO(p))
            .ToListAsync();
    }

    public async Task<PatientResponseDTO> GetByIdAsync(int id)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.Id == id && p.IsActive)
            ?? throw new KeyNotFoundException($"Patient with ID {id} not found.");

        return MapToDTO(patient);
    }

    public async Task<PatientResponseDTO> CreateAsync(CreatePatientDTO dto)
    {
        var exists = await _context.Patients
            .AnyAsync(p => p.Email == dto.Email.ToLower());

        if (exists)
            throw new InvalidOperationException("A patient with this email already exists.");

        var patient = new Patient
        {
            FullName = dto.FullName.Trim(),
            Email = dto.Email.ToLower().Trim(),
            Phone = dto.Phone.Trim(),
            DateOfBirth = dto.DateOfBirth,
            Gender = dto.Gender,
            BloodGroup = dto.BloodGroup.Trim(),
            Address = dto.Address.Trim()
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        return MapToDTO(patient);
    }

    public async Task<PatientResponseDTO> UpdateAsync(int id, UpdatePatientDTO dto)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.Id == id && p.IsActive)
            ?? throw new KeyNotFoundException($"Patient with ID {id} not found.");

        patient.FullName = dto.FullName.Trim();
        patient.Phone = dto.Phone.Trim();
        patient.Address = dto.Address.Trim();
        patient.BloodGroup = dto.BloodGroup.Trim();
        patient.Gender = dto.Gender;
        patient.DateOfBirth = dto.DateOfBirth;
        patient.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return MapToDTO(patient);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.Id == id && p.IsActive)
            ?? throw new KeyNotFoundException($"Patient with ID {id} not found.");

        patient.IsActive = false;
        patient.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    private static PatientResponseDTO MapToDTO(Patient p) => new()
    {
        Id = p.Id,
        FullName = p.FullName,
        Email = p.Email,
        Phone = p.Phone,
        DateOfBirth = p.DateOfBirth,
        Gender = p.Gender,
        BloodGroup = p.BloodGroup,
        Address = p.Address,
        RegisteredAt = p.CreatedAt
    };
}