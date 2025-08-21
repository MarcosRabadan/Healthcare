using Healthcare.Application.Services;
using Healthcare.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Healthcare.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlergiasController : ControllerBase
    {
        private readonly AlergiaService _alergiaService;

        public AlergiasController(AlergiaService alergiaService)
        {
            _alergiaService = alergiaService;
        }

        // GET: /api/alergias/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Alergia>> GetAlergia(int id)
        {
            var alergia = await _alergiaService.GetByIdAsync(id);
            if (alergia == null)
                return NotFound();

            return Ok(alergia);
        }

        // POST: /api/alergias
        [HttpPost]
        public async Task<ActionResult<Alergia>> CreateAlergia([FromBody] Alergia alergia)
        {
            var created = await _alergiaService.CreateAsync(alergia);
            return CreatedAtAction(nameof(GetAlergia), new { id = created.Id }, created);
        }

        // PUT: /api/alergias/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAlergia(int id, [FromBody] Alergia alergia)
        {
            var updated = await _alergiaService.UpdateAsync(id, alergia);
            if (!updated)
                return NotFound();

            return NoContent();
        }
        // DELETE: /api/alergias/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAlergia(int id)
        {
            var deleted = await _alergiaService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}