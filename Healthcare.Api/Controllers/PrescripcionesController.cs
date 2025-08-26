using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Application.Services;
using Healthcare.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescripcionesController : ControllerBase
    {
        private readonly PrescripcionService _prescripcionService;

        public PrescripcionesController(PrescripcionService prescripcionService)
        {
            _prescripcionService = prescripcionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrescripcionResponseDto>>> GetPrescripciones()
        {
            var prescripciones = await _prescripcionService.GetAllAsync();
            return Ok(prescripciones);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PrescripcionResponseDto>> GetPrescripcion(int id)
        {
            var prescripcion = await _prescripcionService.GetByIdAsync(id);
            if (prescripcion == null)
                return NotFound();
            return Ok(prescripcion);
        }

        [HttpPost]
        public async Task<ActionResult<PrescripcionResponseDto>> CreatePrescripcion([FromBody] PrescripcionRequestDto prescripcion)
        {
            var created = await _prescripcionService.CreateAsync(prescripcion);
            return CreatedAtAction(nameof(GetPrescripcion), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePrescripcion(int id, [FromBody] PrescripcionRequestDto prescripcion)
        {
            var updated = await _prescripcionService.UpdateAsync(id, prescripcion);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePrescripcion(int id)
        {
            var deleted = await _prescripcionService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}