using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using IncludIA.Application.Services;
using IncludIA.Domain.Entities;

namespace IncludIA.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class VagaController : ControllerBase
    {
        private readonly VagaService _service;

        public VagaController(VagaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var vagas = await _service.GetAllAsync(page, size);
            
            var resultado = vagas.Select(v => new 
            {
                Data = v,
                Links = new[] 
                {
                    new { rel = "self", href = $"/api/v1/vaga/{v.Id}", method = "GET" },
                    new { rel = "update", href = $"/api/v1/vaga/{v.Id}", method = "PUT" },
                    new { rel = "delete", href = $"/api/v1/vaga/{v.Id}", method = "DELETE" }
                }
            });

            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Vaga vaga)
        {
            await _service.CreateAsync(vaga);
            return CreatedAtAction(nameof(GetAll), null, vaga);
        }
    }
}