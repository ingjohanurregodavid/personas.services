using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using personas.services.Application.DTOs;
using personas.services.Application.Interfaces;

namespace personas.services.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaService _personaService;

        public PersonasController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(PersonaDTO persona)
        {
            var result = await _personaService.AddAsync(persona);
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var persona = await _personaService.GetByIdAsync(id);
            return Ok(persona);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _personaService.GetAllAsync();
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(PersonaDTO persona)
        {
            await _personaService.UpdateAsync(persona);
            return Ok("Actualización exitosa");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _personaService.DeleteAsync(id);
            return Ok("Registro Eliminado");
        }
    }
}
