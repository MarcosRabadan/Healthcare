using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacientesController(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        // GET: /api/pacientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
        {
            var pacientes = await _pacienteRepository.GetAllAsync();
            return Ok(pacientes);
        }
        // GET: /api/pacientes/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Paciente>> GetPaciente(int id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return Ok(paciente);
        }
    }
}