using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediSync.API.DTOs.MedicalRecord;
using MediSync.API.Helpers;
using MediSync.API.Services.Interfaces;

namespace MediSync.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MedicalRecordController : ControllerBase
{
    private readonly IMedicalRecordService _medicalRecordService;

    public MedicalRecordController(IMedicalRecordService medicalRecordService)
    {
        _medicalRecordService = medicalRecordService;
    }

    /// <summary>Get all medical records</summary>
    [HttpGet]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _medicalRecordService.GetAllAsync();
        return Ok(ApiResponse<List<MedicalRecordResponseDTO>>.Ok(result));
    }

    /// <summary>Get all medical records for a patient</summary>
    [HttpGet("patient/{patientId}")]
    [Authorize(Roles = "Admin,Doctor,Patient")]
    public async Task<IActionResult> GetByPatient(int patientId)
    {
        try
        {
            var result = await _medicalRecordService.GetByPatientIdAsync(patientId);
            return Ok(ApiResponse<List<MedicalRecordResponseDTO>>.Ok(result));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<string>.Fail(ex.Message));
        }
    }

    /// <summary>Get medical record by ID</summary>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Doctor,Patient")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _medicalRecordService.GetByIdAsync(id);
            return Ok(ApiResponse<MedicalRecordResponseDTO>.Ok(result));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<string>.Fail(ex.Message));
        }
    }

    /// <summary>Add a new medical record for a patient</summary>
    [HttpPost]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> Create([FromBody] CreateMedicalRecordDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<string>.Fail("Validation failed."));

        try
        {
            var result = await _medicalRecordService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),
                new { id = result.Id },
                ApiResponse<MedicalRecordResponseDTO>.Ok(result, "Medical record added successfully."));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<string>.Fail(ex.Message));
        }
    }

    /// <summary>Update a medical record</summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMedicalRecordDTO dto)
    {
        try
        {
            var result = await _medicalRecordService.UpdateAsync(id, dto);
            return Ok(ApiResponse<MedicalRecordResponseDTO>.Ok(result, "Medical record updated successfully."));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<string>.Fail(ex.Message));
        }
    }

    /// <summary>Delete a medical record</summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _medicalRecordService.DeleteAsync(id);
            return Ok(ApiResponse<string>.Ok("Deleted", "Medical record deleted successfully."));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<string>.Fail(ex.Message));
        }
    }
}