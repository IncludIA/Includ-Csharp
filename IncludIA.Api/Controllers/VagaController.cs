using Asp.Versioning;
using IncludIA.Application.Service;
using IncludIA.Domain.Entities;
using IncludIA.Api.DTOs.Vaga;
using IncludIA.Api.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace IncludIA.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/vaga")]
    public class VagaController : ControllerBase
    {
        private readonly JobVagaService _service;

        public VagaController(JobVagaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var vagas = await _service.GetAllAsync(page, size);
            
            var response = vagas.Select(v => new JobVagaResponse
            {
                Id = v.Id,
                Titulo = v.Titulo,
                Descricao = v.DescricaoInclusiva ?? v.DescricaoOriginal,
                IsDescricaoInclusiva = !string.IsNullOrEmpty(v.DescricaoInclusiva),
                Localizacao = v.Localizacao,
                TipoVaga = v.TipoVaga.ToString(),
                ModeloTrabalho = v.ModeloTrabalho.ToString(),
                SalarioMin = v.SalarioMin,
                SalarioMax = v.SalarioMax,
                Beneficios = v.Beneficios,
                IsAtiva = v.IsAtiva,
                CreatedAt = v.CreatedAt,
                NomeEmpresa = v.Empresa?.NomeFantasia ?? "Confidencial",
                NomeRecrutador = v.Recruiter?.Nome,
                Skills = v.SkillsDesejadas.Select(s => s.Nome).ToList(),
                Links = GenerateLinks(v.Id)
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var v = await _service.GetByIdAsync(id);
            if (v == null) return NotFound();

            var response = new JobVagaResponse
            {
                Id = v.Id,
                Titulo = v.Titulo,
                Descricao = v.DescricaoInclusiva ?? v.DescricaoOriginal,
                IsDescricaoInclusiva = !string.IsNullOrEmpty(v.DescricaoInclusiva),
                Localizacao = v.Localizacao,
                TipoVaga = v.TipoVaga.ToString(),
                ModeloTrabalho = v.ModeloTrabalho.ToString(),
                SalarioMin = v.SalarioMin,
                SalarioMax = v.SalarioMax,
                Beneficios = v.Beneficios,
                IsAtiva = v.IsAtiva,
                CreatedAt = v.CreatedAt,
                NomeEmpresa = v.Empresa?.NomeFantasia ?? "Confidencial",
                NomeRecrutador = v.Recruiter?.Nome,
                Skills = v.SkillsDesejadas.Select(s => s.Nome).ToList(),
                Links = GenerateLinks(v.Id)
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JobVagaRequest request)
        {
            var vaga = new JobVaga
            {
                Id = Guid.NewGuid(),
                Titulo = request.Titulo,
                DescricaoOriginal = request.DescricaoOriginal,
                Localizacao = request.Localizacao,
                TipoVaga = request.TipoVaga,
                ModeloTrabalho = request.ModeloTrabalho,
                SalarioMin = request.SalarioMin,
                SalarioMax = request.SalarioMax,
                Beneficios = request.Beneficios,
                ExperienciaRequerida = request.ExperienciaRequerida,
                RecruiterId = request.RecruiterId,
                EmpresaId = request.EmpresaId,
                IsAtiva = true,
                CreatedAt = DateTime.UtcNow
            };
            
            await _service.CreateAsync(vaga);

            return CreatedAtAction(nameof(GetById), new { id = vaga.Id }, vaga);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] JobVagaRequest request)
        {
            var existingVaga = await _service.GetByIdAsync(id);
            if (existingVaga == null) return NotFound();
            
            existingVaga.Titulo = request.Titulo;
            existingVaga.DescricaoOriginal = request.DescricaoOriginal;
            existingVaga.Localizacao = request.Localizacao;
            existingVaga.TipoVaga = request.TipoVaga;
            existingVaga.ModeloTrabalho = request.ModeloTrabalho;
            existingVaga.SalarioMin = request.SalarioMin;
            existingVaga.SalarioMax = request.SalarioMax;
            existingVaga.Beneficios = request.Beneficios;
            existingVaga.ExperienciaRequerida = request.ExperienciaRequerida;

            await _service.UpdateAsync(existingVaga);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        private List<LinkDto> GenerateLinks(Guid id)
        {
            return new List<LinkDto>
            {
                new LinkDto(Url.Action(nameof(GetById), new { id }) ?? "", "self", "GET"),
                new LinkDto(Url.Action(nameof(Update), new { id }) ?? "", "update", "PUT"),
                new LinkDto(Url.Action(nameof(Delete), new { id }) ?? "", "delete", "DELETE"),
                new LinkDto($"/api/v1/match/create?jobVagaId={id}", "apply", "POST")
            };
        }
    }
}