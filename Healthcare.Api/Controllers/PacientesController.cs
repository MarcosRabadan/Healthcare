using Healthcare.Domain.Entities;
using Healthcare.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Healthcare.Application.DTOs;

namespace Healthcare.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly PacienteService _pacienteService;

        public PacientesController(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        // GET: /api/pacientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteDto>>> GetPacientes()
        {
            var pacientes = await _pacienteService.GetAllAsync();
            return Ok(pacientes);
        }

        // GET: /api/pacientes/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PacienteDto>> GetPaciente(int id)
        {
            var paciente = await _pacienteService.GetByIdAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return Ok(paciente);
        }

        // POST: /api/pacientes     
        [HttpPost]
        public async Task<ActionResult<PacienteDto>> CreatePaciente([FromBody] PacienteDto paciente)
        {
            var created = await _pacienteService.CreateAsync(paciente);
            return CreatedAtAction(nameof(GetPaciente), new { id = created.Id }, created);
        }

        // PUT: /api/pacientes/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePaciente(int id, [FromBody] PacienteDto paciente)
        {
            var updated = await _pacienteService.UpdateAsync(id, paciente);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: /api/pacientes/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var deleted = await _pacienteService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}