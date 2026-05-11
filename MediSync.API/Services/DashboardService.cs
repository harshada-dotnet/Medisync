using Microsoft.EntityFrameworkCore;
using MediSync.API.Data;
using MediSync.API.DTOs.Dashboard;
using MediSync.API.Models.Appointment;
using MediSync.API.Services.Interfaces;

namespace MediSync.API.Services;

public class DashboardService : IDashboardService
{
    private readonly ApplicationDbContext _context;

    public DashboardService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardStatsDTO> GetStatsAsync()
    {
        var today = DateTime.UtcNow.Date;

        // Core counts
        var totalPatients = await _context.Patients
            .CountAsync(p => p.IsActive);

        var totalDoctors = await _context.Doctors
            .CountAsync();

        var totalAppointments = await _context.Appointments
            .CountAsync();

        var todaysAppointments = await _context.Appointments
            .CountAsync(a => a.AppointmentDate.Date == today);

        // Appointments by status
        var pendingCount = await _context.Appointments
            .CountAsync(a => a.Status == AppointmentStatus.Pending);

        var confirmedCount = await _context.Appointments
            .CountAsync(a => a.Status == AppointmentStatus.Confirmed);

        var completedCount = await _context.Appointments
            .CountAsync(a => a.Status == AppointmentStatus.Completed);

        var cancelledCount = await _context.Appointments
            .CountAsync(a => a.Status == AppointmentStatus.Cancelled);

        // Recent 5 appointments
        var recentAppointments = await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .OrderByDescending(a => a.CreatedAt)
            .Take(5)
            .Select(a => new RecentAppointmentDTO
            {
                Id = a.Id,
                PatientName = a.Patient.FullName,
                DoctorName = a.Doctor.FullName,
                DoctorSpecialization = a.Doctor.Specialization,
                AppointmentDate = a.AppointmentDate,
                TimeSlot = a.TimeSlot,
                Status = a.Status.ToString()
            })
            .ToListAsync();

        // Top 5 doctors by appointment count
        var topDoctors = await _context.Appointments
            .Include(a => a.Doctor)
            .GroupBy(a => new
            {
                a.DoctorId,
                a.Doctor.FullName,
                a.Doctor.Specialization
            })
            .Select(g => new TopDoctorDTO
            {
                DoctorId = g.Key.DoctorId,
                FullName = g.Key.FullName,
                Specialization = g.Key.Specialization,
                TotalAppointments = g.Count()
            })
            .OrderByDescending(d => d.TotalAppointments)
            .Take(5)
            .ToListAsync();

        return new DashboardStatsDTO
        {
            TotalPatients = totalPatients,
            TotalDoctors = totalDoctors,
            TotalAppointments = totalAppointments,
            TodaysAppointments = todaysAppointments,
            PendingAppointments = pendingCount,
            ConfirmedAppointments = confirmedCount,
            CompletedAppointments = completedCount,
            CancelledAppointments = cancelledCount,
            RecentAppointments = recentAppointments,
            TopDoctors = topDoctors
        };
    }
}