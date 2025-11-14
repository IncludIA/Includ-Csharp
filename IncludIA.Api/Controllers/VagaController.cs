using Asp.Versioning;
using IncludIA.Application.Service;
using IncludIA.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IncludIA.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class VagaController : ControllerBase
    {
        private readonly VagaService _vagaService;
        private readonly InclusaoService _inclusaoService; 

        public VagaController(VagaService vagaService, InclusaoService inclusaoService)
        {
            _vagaService = vagaService;
            _inclusaoService = inclusaoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var vagas = await _vagaService.GetAllAsync(page, size);

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
            vaga.Descricao = await _inclusaoService.TornarDescricaoInclusivaAsync(vaga.Descricao);
            await _vagaService.CreateAsync(vaga);
            return CreatedAtAction(nameof(GetAll), new { id = vaga.Id }, vaga);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var vaga = await _vagaService.GetByIdAsync(id);
            if (vaga == null)
            {
                return NotFound();
            }
            return Ok(vaga);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Vaga vagaIn)
        {
            var vaga = await _vagaService.GetByIdAsync(id);
            if (vaga == null)
            {
                return NotFound();
            }
            vagaIn.Descricao = await _inclusaoService.TornarDescricaoInclusivaAsync(vagaIn.Descricao);
            await _vagaService.UpdateAsync(id, vagaIn);
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var vaga = await _vagaService.GetByIdAsync(id);
            if (vaga == null)
            {
                return NotFound();
            }

            await _vagaService.DeleteAsync(id);
            return NoContent();
        }
    }
}