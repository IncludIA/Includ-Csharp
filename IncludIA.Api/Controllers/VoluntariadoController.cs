using Asp.Versioning;
using IncludIA.Application.Service;
using IncludIA.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IncludIA.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VoluntariadoController : ControllerBase
    {
        private readonly VoluntariadoService _service;
        public VoluntariadoController(VoluntariadoService service) => _service = service;

        [HttpGet("candidate/{candidateId}")]
        public async Task<IActionResult> GetByCandidate(Guid candidateId) => Ok(await _service.GetByCandidateIdAsync(candidateId));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Voluntariado voluntariado)
        {
            await _service.AddVoluntariadoAsync(voluntariado);
            return Ok(voluntariado);
        }
    }
}