using Healthcare.Application.DTOs;
using Healthcare.Application.Services;
using Healthcare.Domain.Entities;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitaDto>>> GetCitas()
        {
            var citas = await _citaService.GetAllAsync();
            return Ok(citas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CitaDto>> GetCita(int id)
        {
            var cita = await _citaService.GetByIdAsync(id);
            if (cita == null)
                return NotFound();
            return Ok(cita);
        }

        [HttpPost]
        public async Task<ActionResult<CitaDto>> CreateCita([FromBody] CitaDto cita)
        {
            var created = await _citaService.CreateAsync(cita);
            return CreatedAtAction(nameof(GetCita), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCita(int id, [FromBody] CitaDto cita)
        {
            var updated = await _citaService.UpdateAsync(id, cita);
            if (!updated)
                return NotFound();
            return NoContent();
        }

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