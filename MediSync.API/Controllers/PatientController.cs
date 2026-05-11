using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediSync.API.DTOs.Patient;
using MediSync.API.Helpers;
using MediSync.API.Services.Interfaces;

namespace MediSync.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> GetAll([FromQuery] string? search)
    {
        var result = await _patientService.GetAllAsync(search);
        return Ok(ApiResponse<List<PatientResponseDTO>>.Ok(result));
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _patientService.GetByIdAsync(id);
            return Ok(ApiResponse<PatientResponseDTO>.Ok(result));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<string>.Fail(ex.Message));
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> Create([FromBody] CreatePatientDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<string>.Fail("Validation failed."));

        try
        {
            var result = await _patientService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),
                new { id = result.Id },
                ApiResponse<PatientResponseDTO>.Ok(result, "Patient registered successfully."));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ApiResponse<string>.Fail(ex.Message));
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePatientDTO dto)
    {
        try
        {
            var result = await _patientService.UpdateAsync(id, dto);
            return Ok(ApiResponse<PatientResponseDTO>.Ok(result, "Patient updated successfully."));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<string>.Fail(ex.Message));
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _patientService.DeleteAsync(id);
            return Ok(ApiResponse<string>.Ok("Deleted", "Patient deleted successfully."));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<string>.Fail(ex.Message));
        }
    }
}