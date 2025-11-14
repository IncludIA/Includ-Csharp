using Asp.Versioning;
using IncludIA.Application.Service;
using IncludIA.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IncludIA.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CandidatoController : ControllerBase
    {
        private readonly CandidatoService _service;

        public CandidatoController(CandidatoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int size = 10)
        {
            var candidatos = await _service.GetAllAsync(page, size);
            return Ok(candidatos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Candidato candidato)
        {
            await _service.CreateAsync(candidato);
            return CreatedAtAction(nameof(GetById), new { id = candidato.Id }, candidato);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var candidato = await _service.GetByIdAsync(id);
            if (candidato == null) return NotFound();
            return Ok(candidato);
        }
    }
}