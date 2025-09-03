using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsersController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: api/users
        [Authorize(Roles = "Admin,Administrativo")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _usuarioService.GetAllAsync();
            return Ok(users);
        }

        // GET: api/users/{id}
        [Authorize(Roles = "Admin,Administrativo")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _usuarioService.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST: api/users
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioRequestDto  request)
        {
            var result = await _usuarioService.CreateAsync(request);
            if (result.Error != null)
                return BadRequest(result.Error);
            return CreatedAtAction(nameof(GetById), new { id = result.Created!.Id }, result.Created);
        }

        // PUT: api/users/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioRequestDto request)
        {
            var result = await _usuarioService.UpdateAsync(id, request);

            if (result.Error != null)
                return BadRequest(result.Error);
            if (!result.Success)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/users/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _usuarioService.DeleteAsync(id);
            if(!deleted)
                return NotFound();   

            return NoContent();
        }
    }
}
