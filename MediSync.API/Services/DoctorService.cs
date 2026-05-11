using MediSync.API.Data;
using MediSync.API.DTOs.Doctor;
using MediSync.API.Models.Doctor;
using MediSync.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace MediSync.API.Services;

public class DoctorService : IDoctorService
{
    private readonly ApplicationDbContext _context;

    public DoctorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DoctorResponseDTO>> GetAllAsync(string? search)
    {
        var query = _context.Doctors.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(d =>
                d.FullName.Contains(search) ||
                d.Specialization.Contains(search) ||
                d.Email.Contains(search));

        return await query
            .OrderByDescending(d => d.CreatedAt)
            .Select(d => MapToDTO(d))
            .ToListAsync();
    }

    public async Task<DoctorResponseDTO> GetByIdAsync(int id)
    {
        var doctor = await _context.Doctors
            .FirstOrDefaultAsync(d => d.Id == id)
            ?? throw new KeyNotFoundException($"Doctor with ID {id} not found.");

        return MapToDTO(doctor);
    }

    public async Task<DoctorResponseDTO> CreateAsync(CreateDoctorDTO dto)
    {
        var exists = await _context.Doctors
            .AnyAsync(d => d.Email == dto.Email.ToLower());

        if (exists)
            throw new InvalidOperationException("A doctor with this email already exists.");

        var doctor = new Doctor
        {
            FullName = dto.FullName.Trim(),
            Email = dto.Email.ToLower().Trim(),
            Phone = dto.Phone.Trim(),
            Specialization = dto.Specialization.Trim(),
            Qualification = dto.Qualification.Trim(),
            ExperienceYears = dto.ExperienceYears
        };

        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
        return MapToDTO(doctor);
    }

    public async Task<DoctorResponseDTO> UpdateAsync(int id, UpdateDoctorDTO dto)
    {
        var doctor = await _context.Doctors
            .FirstOrDefaultAsync(d => d.Id == id)
            ?? throw new KeyNotFoundException($"Doctor with ID {id} not found.");

        doctor.FullName = dto.FullName.Trim();
        doctor.Phone = dto.Phone.Trim();
        doctor.Specialization = dto.Specialization.Trim();
        doctor.Qualification = dto.Qualification.Trim();
        doctor.ExperienceYears = dto.ExperienceYears;
        doctor.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return MapToDTO(doctor);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var doctor = await _context.Doctors
            .FirstOrDefaultAsync(d => d.Id == id)
            ?? throw new KeyNotFoundException($"Doctor with ID {id} not found.");

        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();
        return true;
    }

    private static DoctorResponseDTO MapToDTO(Doctor d) => new()
    {
        Id = d.Id,
        FullName = d.FullName,
        Email = d.Email,
        Phone = d.Phone,
        Specialization = d.Specialization,
        Qualification = d.Qualification,
        ExperienceYears = d.ExperienceYears,
        IsAvailable = d.IsAvailable
    };
}