using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Application.Services;
using Healthcare.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnamnesisController : ControllerBase
    {
        private readonly AnamnesisService _anamnesisService;

        public AnamnesisController(AnamnesisService anamnesisService)
        {
            _anamnesisService = anamnesisService;
        }

        // GET: /api/anamnesis
        [Authorize(Roles = "Admin,Administrativo")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnamnesisResponseDto>>> GetAnamnesis()
        {
            var anamnesis = await _anamnesisService.GetAllAsync();
            return Ok(anamnesis);
        }

        // GET: /api/anamnesis/{id}
        [Authorize(Roles = "Admin,Administrativo")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AnamnesisResponseDto>> GetAnamnesisById(int id)
        {
            var anamnesis = await _anamnesisService.GetByIdAsync(id);
            if (anamnesis == null)
                return NotFound();
            return Ok(anamnesis);
        }

        // POST: /api/anamnesis
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<AnamnesisResponseDto>> CreateAnamnesis([FromBody] AnamnesisRequestDto anamnesis)
        {
            var created = await _anamnesisService.CreateAsync(anamnesis);
            return CreatedAtAction(nameof(GetAnamnesisById), new { id = created.Id }, created);
        }

        // PUT: /api/anamnesis/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAnamnesis(int id, [FromBody] AnamnesisRequestDto anamnesis)
        {
            var updated = await _anamnesisService.UpdateAsync(id, anamnesis);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        // DELETE: /api/anamnesis/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAnamnesis(int id)
        {
            var deleted = await _anamnesisService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}