using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediSync.API.DTOs.Doctor;
using MediSync.API.Helpers;
using MediSync.API.Services.Interfaces;

namespace MediSync.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] string? search)
    {
        var result = await _doctorService.GetAllAsync(search);
        return Ok(ApiResponse<List<DoctorResponseDTO>>.Ok(result));
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _doctorService.GetByIdAsync(id);
            return Ok(ApiResponse<DoctorResponseDTO>.Ok(result));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<string>.Fail(ex.Message));
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> Create([FromBody] CreateDoctorDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<string>.Fail("Validation failed."));

        try
        {
            var result = await _doctorService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),
                new { id = result.Id },
                ApiResponse<DoctorResponseDTO>.Ok(result, "Doctor added successfully."));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ApiResponse<string>.Fail(ex.Message));
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDoctorDTO dto)
    {
        try
        {
            var result = await _doctorService.UpdateAsync(id, dto);
            return Ok(ApiResponse<DoctorResponseDTO>.Ok(result, "Doctor updated successfully."));
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
            await _doctorService.DeleteAsync(id);
            return Ok(ApiResponse<string>.Ok("Deleted", "Doctor deleted successfully."));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<string>.Fail(ex.Message));
        }
    }
}