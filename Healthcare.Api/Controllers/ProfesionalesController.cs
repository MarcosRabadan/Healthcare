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
    public class ProfesionalesController : ControllerBase
    {
        private readonly ProfesionalService _profesionalService;

        public ProfesionalesController(ProfesionalService profesionalService)
        {
            _profesionalService = profesionalService;
        }

        [Authorize(Roles = "Admin,Administrativo")]
        [HttpGet]
        public async Task<IActionResult> GetProfesionales([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = _profesionalService.GetAll();
            var pagedResult = await PaginacionUtils.PaginateAsync(query, pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [Authorize(Roles = "Admin,Administrativo")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProfesionalResponseDto>> GetProfesional(int id)
        {
            var profesional = await _profesionalService.GetByIdAsync(id);
            if (profesional == null)
                return NotFound();
            return Ok(profesional);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ProfesionalResponseDto>> CreateProfesional([FromBody] ProfesionalRequestDto profesional)
        {
            var created = await _profesionalService.CreateAsync(profesional);
            return CreatedAtAction(nameof(GetProfesional), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProfesional(int id, [FromBody] ProfesionalRequestDto profesional)
        {
            var updated = await _profesionalService.UpdateAsync(id, profesional);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
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