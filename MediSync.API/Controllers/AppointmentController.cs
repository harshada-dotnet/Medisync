using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediSync.API.DTOs.Appointment;
using MediSync.API.Helpers;
using MediSync.API.Services.Interfaces;

namespace MediSync.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    /// <summary>Get all appointments</summary>
    [HttpGet]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _appointmentService.GetAllAsync();
        return Ok(ApiResponse<List<AppointmentResponseDTO>>.Ok(result));
    }

    /// <summary>Get appointments by patient ID</summary>
    [HttpGet("patient/{patientId}")]
    [Authorize(Roles = "Admin,Doctor,Patient")]
    public async Task<IActionResult> GetByPatient(int patientId)
    {
        var result = await _appointmentService.GetByPatientIdAsync(patientId);
        return Ok(ApiResponse<List<AppointmentResponseDTO>>.Ok(result));
    }

    /// <summary>Get appointments by doctor ID</summary>
    [HttpGet("doctor/{doctorId}")]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> GetByDoctor(int doctorId)
    {
        var result = await _appointmentService.GetByDoctorIdAsync(doctorId);
        return Ok(ApiResponse<List<AppointmentResponseDTO>>.Ok(result));
    }

    /// <summary>Get appointment by ID</summary>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Doctor,Patient")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _appointmentService.GetByIdAsync(id);
            return Ok(ApiResponse<AppointmentResponseDTO>.Ok(result));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<string>.Fail(ex.Message));
        }
    }

    /// <summary>Book a new appointment</summary>
    [HttpPost]
    [Authorize(Roles = "Admin,Doctor,Patient")]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<string>.Fail("Validation failed."));

        try
        {
            var result = await _appointmentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),
                new { id = result.Id },
                ApiResponse<AppointmentResponseDTO>.Ok(result, "Appointment booked successfully."));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<string>.Fail(ex.Message));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ApiResponse<string>.Fail(ex.Message));
        }
    }

    /// <summary>Update appointment status</summary>
    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateAppointmentStatusDTO dto)
    {
        try
        {
            var result = await _appointmentService.UpdateStatusAsync(id, dto);
            return Ok(ApiResponse<AppointmentResponseDTO>.Ok(result, "Appointment status updated."));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<string>.Fail(ex.Message));
        }
    }

    /// <summary>Delete an appointment</summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _appointmentService.DeleteAsync(id);
            return Ok(ApiResponse<string>.Ok("Deleted", "Appointment deleted successfully."));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<string>.Fail(ex.Message));
        }
    }
}