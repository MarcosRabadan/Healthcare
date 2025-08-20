using Healthcare.Domain.Entities;
using Healthcare.Application.Services;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
        {
            var pacientes = await _pacienteService.GetAllAsync();
            return Ok(pacientes);
        }

        // GET: /api/pacientes/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Paciente>> GetPaciente(int id)
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
        public async Task<ActionResult<Paciente>> CreatePaciente([FromBody] Paciente paciente)
        {
            var created = await _pacienteService.CreateAsync(paciente);
            return CreatedAtAction(nameof(GetPaciente), new { id = created.Id }, created);
        }
    }
}