// Similar ao Education, injetando ExperienceService
using Asp.Versioning;
using IncludIA.Application.Service;
using IncludIA.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IncludIA.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ExperienceController : ControllerBase
    {
        private readonly ExperienceService _service;
        public ExperienceController(ExperienceService service) => _service = service;

        [HttpGet("candidate/{candidateId}")]
        public async Task<IActionResult> GetByCandidate(Guid candidateId) => Ok(await _service.GetByCandidateIdAsync(candidateId));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Experience experience)
        {
            await _service.AddExperienceAsync(experience);
            return Ok(experience);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteExperienceAsync(id);
            return NoContent();
        }
    }
}