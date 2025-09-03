using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Application.Services;
using Healthcare.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly CitaService _citaService;

        public CitasController(CitaService citaService)
        {
            _citaService = citaService;
        }

        [Authorize(Roles = "Admin,Administrativo")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitaResponseDto>>> GetCitas()
        {
            var citas = await _citaService.GetAllAsync();
            return Ok(citas);
        }

        [Authorize(Roles = "Admin,Administrativo")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CitaResponseDto>> GetCita(int id)
        {
            var cita = await _citaService.GetByIdAsync(id);
            if (cita == null)
                return NotFound();
            return Ok(cita);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CitaResponseDto>> CreateCita([FromBody] CitaRequestDto cita)
        {
            var result = await _citaService.CreateAsync(cita);

            if (result.Error != null)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetCita), new { id = result.Created!.Id }, result.Created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCita(int id, [FromBody] CitaRequestDto cita)
        {
            var updated = await _citaService.UpdateAsync(id, cita);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            var deleted = await _citaService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}