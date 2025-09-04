using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Application.Services;
using Healthcare.Application.Utils;
using Healthcare.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize(Roles = "Admin,Administrativo")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteResponseDto>>> GetPacientes([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = _pacienteService.GetAll();
            var pagedResult = await PaginacionUtils.PaginateAsync(query, pageNumber, pageSize);
            return Ok(pagedResult);
        }

        // GET: /api/pacientes/{id}
        [Authorize(Roles = "Admin,Administrativo")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PacienteResponseDto>> GetPaciente(int id)
        {
            var paciente = await _pacienteService.GetByIdAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return Ok(paciente);
        }

        // POST: /api/pacientes     
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<PacienteResponseDto>> CreatePaciente([FromBody] PacienteRequestDto paciente)
        {
            var result = await _pacienteService.CreateAsync(paciente);

            if (result.Error != null)
                return BadRequest(result.Error);
  
            return CreatedAtAction(nameof(GetPaciente), new { id = result.Created!.Id }, result.Created);
        }
         
           
       
        // PUT: /api/pacientes/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePaciente(int id, [FromBody] PacienteRequestDto paciente)
        {
            var result = await _pacienteService.UpdateAsync(id, paciente);

            if (result.Error != null)
                return BadRequest(result.Error);

            if (!result.Success)
                return NotFound();

            return NoContent();
        }

        // DELETE: /api/pacientes/{id}
        [Authorize(Roles = "Admin")]
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