using Microsoft.EntityFrameworkCore;
using MediSync.API.Data;
using MediSync.API.DTOs.Appointment;
using MediSync.API.Models.Appointment;
using MediSync.API.Services.Interfaces;

namespace MediSync.API.Services;

public class AppointmentService : IAppointmentService
{
    private readonly ApplicationDbContext _context;

    public AppointmentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AppointmentResponseDTO>> GetAllAsync()
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .OrderByDescending(a => a.AppointmentDate)
            .Select(a => MapToDTO(a))
            .ToListAsync();
    }

    public async Task<List<AppointmentResponseDTO>> GetByPatientIdAsync(int patientId)
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.PatientId == patientId)
            .OrderByDescending(a => a.AppointmentDate)
            .Select(a => MapToDTO(a))
            .ToListAsync();
    }

    public async Task<List<AppointmentResponseDTO>> GetByDoctorIdAsync(int doctorId)
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.DoctorId == doctorId)
            .OrderByDescending(a => a.AppointmentDate)
            .Select(a => MapToDTO(a))
            .ToListAsync();
    }

    public async Task<AppointmentResponseDTO> GetByIdAsync(int id)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(a => a.Id == id)
            ?? throw new KeyNotFoundException($"Appointment with ID {id} not found.");

        return MapToDTO(appointment);
    }

    public async Task<AppointmentResponseDTO> CreateAsync(CreateAppointmentDTO dto)
    {
        // Check patient exists
        var patientExists = await _context.Patients
            .AnyAsync(p => p.Id == dto.PatientId && p.IsActive);

        if (!patientExists)
            throw new KeyNotFoundException($"Patient with ID {dto.PatientId} not found.");

        // Check doctor exists
        var doctorExists = await _context.Doctors
            .AnyAsync(d => d.Id == dto.DoctorId && d.IsAvailable);

        if (!doctorExists)
            throw new KeyNotFoundException($"Doctor with ID {dto.DoctorId} not found or unavailable.");

        // Check no duplicate appointment same doctor same date same timeslot
        var duplicate = await _context.Appointments
            .AnyAsync(a =>
                a.DoctorId == dto.DoctorId &&
                a.AppointmentDate.Date == dto.AppointmentDate.Date &&
                a.TimeSlot == dto.TimeSlot &&
                a.Status != AppointmentStatus.Cancelled);

        if (duplicate)
            throw new InvalidOperationException("This time slot is already booked for the doctor.");

        var appointment = new Appointment
        {
            PatientId = dto.PatientId,
            DoctorId = dto.DoctorId,
            AppointmentDate = dto.AppointmentDate,
            TimeSlot = dto.TimeSlot.Trim(),
            Notes = dto.Notes.Trim(),
            Status = AppointmentStatus.Pending
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        // Reload with navigation properties
        return await GetByIdAsync(appointment.Id);
    }

    public async Task<AppointmentResponseDTO> UpdateStatusAsync(int id, UpdateAppointmentStatusDTO dto)
    {
        var appointment = await _context.Appointments
            .FirstOrDefaultAsync(a => a.Id == id)
            ?? throw new KeyNotFoundException($"Appointment with ID {id} not found.");

        appointment.Status = dto.Status;
        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var appointment = await _context.Appointments
            .FirstOrDefaultAsync(a => a.Id == id)
            ?? throw new KeyNotFoundException($"Appointment with ID {id} not found.");

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
        return true;
    }

    private static AppointmentResponseDTO MapToDTO(Appointment a) => new()
    {
        Id = a.Id,
        PatientId = a.PatientId,
        PatientName = a.Patient?.FullName ?? string.Empty,
        DoctorId = a.DoctorId,
        DoctorName = a.Doctor?.FullName ?? string.Empty,
        DoctorSpecialization = a.Doctor?.Specialization ?? string.Empty,
        AppointmentDate = a.AppointmentDate,
        TimeSlot = a.TimeSlot,
        Status = a.Status.ToString(),
        Notes = a.Notes,
        CreatedAt = a.CreatedAt
    };
}