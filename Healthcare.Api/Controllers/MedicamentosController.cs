using Healthcare.Application.Services;
using Healthcare.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicamentosController : ControllerBase
    {
        private readonly MedicamentoService _medicamentoService;

        public MedicamentosController(MedicamentoService medicamentoService)
        {
            _medicamentoService = medicamentoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicamento>>> GetMedicamentos()
        {
            var medicamentos = await _medicamentoService.GetAllAsync();
            return Ok(medicamentos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Medicamento>> GetMedicamento(int id)
        {
            var medicamento = await _medicamentoService.GetByIdAsync(id);
            if (medicamento == null)
                return NotFound();
            return Ok(medicamento);
        }

        [HttpPost]
        public async Task<ActionResult<Medicamento>> CreateMedicamento([FromBody] Medicamento medicamento)
        {
            var created = await _medicamentoService.CreateAsync(medicamento);
            return CreatedAtAction(nameof(GetMedicamento), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMedicamento(int id, [FromBody] Medicamento medicamento)
        {
            var updated = await _medicamentoService.UpdateAsync(id, medicamento);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMedicamento(int id)
        {
            var deleted = await _medicamentoService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}