using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Application.Services;
using Healthcare.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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

        // GET: /api/alergias
        [Authorize(Roles = "Admin,Administrativo")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlergiaRequestDto>>> GetAlergias()
        {
            var alergias = await _alergiaService.GetAllAsync();
            return Ok(alergias);
        }

        // GET: /api/alergias/{id}
        [Authorize(Roles = "Admin,Administrativo")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AlergiaResponseDto>> GetAlergia(int id)
        {
            var alergia = await _alergiaService.GetByIdAsync(id);
            if (alergia == null)
                return NotFound();

            return Ok(alergia);
        }

        // POST: /api/alergias
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<AlergiaResponseDto>> CreateAlergia([FromBody] AlergiaRequestDto alergiaDto)
        {
            var result = await _alergiaService.CreateAsync(alergiaDto);

            if (result.Error != null)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetAlergia), new { id = result.Created!.Id }, result.Created);
        }

        // PUT: /api/alergias/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAlergia(int id, [FromBody] AlergiaRequestDto alergia)
        {
            var updated = await _alergiaService.UpdateAsync(id, alergia);
            if (!updated)
                return NotFound();

            return NoContent();
        }
        // DELETE: /api/alergias/{id}
        [Authorize(Roles = "Admin")]
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