using MediSync.API.Models.Appointment;
using MediSync.API.Models.Auth;
using MediSync.API.Models.Doctor;
using MediSync.API.Models.Patient;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace MediSync.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.Email).IsRequired().HasMaxLength(150);
            e.Property(x => x.FullName).IsRequired().HasMaxLength(100);
            e.Property(x => x.Role).IsRequired().HasDefaultValue("Patient");
        });

        modelBuilder.Entity<Patient>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.FullName).IsRequired().HasMaxLength(100);
            e.Property(x => x.Email).IsRequired().HasMaxLength(150);
            e.Property(x => x.Phone).IsRequired().HasMaxLength(15);
            e.Property(x => x.Gender).IsRequired().HasMaxLength(10);
            e.Property(x => x.BloodGroup).HasMaxLength(5);
        });

        modelBuilder.Entity<Doctor>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.FullName).IsRequired().HasMaxLength(100);
            e.Property(x => x.Email).IsRequired().HasMaxLength(150);
            e.Property(x => x.Specialization).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Appointment>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne(x => x.Patient)
             .WithMany()
             .HasForeignKey(x => x.PatientId)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Doctor)
             .WithMany(x => x.Appointments)
             .HasForeignKey(x => x.DoctorId)
             .OnDelete(DeleteBehavior.Restrict);
            e.Property(x => x.Status).HasConversion<string>();
        });

        modelBuilder.Entity<MedicalRecord>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne(x => x.Patient)
             .WithMany(x => x.MedicalRecords)
             .HasForeignKey(x => x.PatientId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }
}