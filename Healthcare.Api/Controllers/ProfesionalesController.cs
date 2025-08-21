using Healthcare.Application.Services;
using Healthcare.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesionalesController : ControllerBase
    {
        private readonly ProfesionalService _profesionalService;

        public ProfesionalesController(ProfesionalService profesionalService)
        {
            _profesionalService = profesionalService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesional>>> GetProfesionales()
        {
            var profesionales = await _profesionalService.GetAllAsync();
            return Ok(profesionales);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Profesional>> GetProfesional(int id)
        {
            var profesional = await _profesionalService.GetByIdAsync(id);
            if (profesional == null)
                return NotFound();
            return Ok(profesional);
        }

        [HttpPost]
        public async Task<ActionResult<Profesional>> CreateProfesional([FromBody] Profesional profesional)
        {
            var created = await _profesionalService.CreateAsync(profesional);
            return CreatedAtAction(nameof(GetProfesional), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProfesional(int id, [FromBody] Profesional profesional)
        {
            var updated = await _profesionalService.UpdateAsync(id, profesional);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProfesional(int id)
        {
            var deleted = await _profesionalService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}