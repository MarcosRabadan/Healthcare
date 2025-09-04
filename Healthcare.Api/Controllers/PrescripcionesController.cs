using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Application.Services;
using Healthcare.Application.Utils;
using Healthcare.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin,Administrativo")]
        [HttpGet]
        public async Task<IActionResult> GetPrescripciones([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = _prescripcionService.GetAll();
            var pagedResult = await PaginacionUtils.PaginateAsync(query, pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [Authorize(Roles = "Admin,Administrativo")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PrescripcionResponseDto>> GetPrescripcion(int id)
        {
            var prescripcion = await _prescripcionService.GetByIdAsync(id);
            if (prescripcion == null)
                return NotFound();
            return Ok(prescripcion);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<PrescripcionResponseDto>> CreatePrescripcion([FromBody] PrescripcionRequestDto prescripcion)
        {
            var created = await _prescripcionService.CreateAsync(prescripcion);
            return CreatedAtAction(nameof(GetPrescripcion), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePrescripcion(int id, [FromBody] PrescripcionRequestDto prescripcion)
        {
            var updated = await _prescripcionService.UpdateAsync(id, prescripcion);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
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