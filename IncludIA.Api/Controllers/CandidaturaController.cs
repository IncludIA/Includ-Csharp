using Asp.Versioning;
using IncludIA.Application.Service;
using IncludIA.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IncludIA.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CandidaturaController : ControllerBase
    {
        private readonly CandidaturaService _service;

        public CandidaturaController(CandidaturaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int size = 10)
        {
            var candidaturas = await _service.GetAllAsync(page, size);
            return Ok(candidaturas);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Candidatura candidatura)
        {
            await _service.CreateAsync(candidatura);
            return CreatedAtAction(nameof(GetById), new { id = candidatura.Id }, candidatura);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var candidatura = await _service.GetByIdAsync(id);
            if (candidatura == null) return NotFound();
            return Ok(candidatura);
        }
    }
}