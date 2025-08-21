using Healthcare.Application.Services;
using Healthcare.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultasController : ControllerBase
    {
        private readonly ConsultaService _consultaService;

        public ConsultasController(ConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetConsultas()
        {
            var consultas = await _consultaService.GetAllAsync();
            return Ok(consultas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Consulta>> GetConsulta(int id)
        {
            var consulta = await _consultaService.GetByIdAsync(id);
            if (consulta == null)
                return NotFound();
            return Ok(consulta);
        }

        [HttpPost]
        public async Task<ActionResult<Consulta>> CreateConsulta([FromBody] Consulta consulta)
        {
            var created = await _consultaService.CreateAsync(consulta);
            return CreatedAtAction(nameof(GetConsulta), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateConsulta(int id, [FromBody] Consulta consulta)
        {
            var updated = await _consultaService.UpdateAsync(id, consulta);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteConsulta(int id)
        {
            var deleted = await _consultaService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}