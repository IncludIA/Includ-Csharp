using Asp.Versioning;
using IncludIA.Application.Service;
using IncludIA.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IncludIA.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class JobVagaController : ControllerBase
    {
        private readonly JobVagaService _service;

        public JobVagaController(JobVagaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var vagas = await _service.GetAllAsync(page, size);
            var response = new
            {
                Data = vagas,
                Links = new[] { new { href = Url.Action(nameof(Create)), rel = "create-vaga", method = "POST" } }
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var vaga = await _service.GetByIdAsync(id);
            if (vaga == null) return NotFound();

            var resource = new
            {
                Data = vaga,
                Links = new[]
                {
                    new { href = Url.Action(nameof(GetById), new { id }), rel = "self", method = "GET" },
                    new { href = Url.Action("Create", "Match", new { jobVagaId = id }), rel = "apply", method = "POST" }
                }
            };
            return Ok(resource);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JobVaga vaga)
        {
            await _service.CreateAsync(vaga);
            return CreatedAtAction(nameof(GetById), new { id = vaga.Id }, vaga);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] JobVaga vaga)
        {
            if (id != vaga.Id) return BadRequest();
            await _service.UpdateAsync(vaga);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}