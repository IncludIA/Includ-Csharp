using Asp.Versioning;
using IncludIA.Application.Service;
using IncludIA.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IncludIA.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly EducationService _service;
        public EducationController(EducationService service) => _service = service;

        [HttpGet("candidate/{candidateId}")]
        public async Task<IActionResult> GetByCandidate(Guid candidateId) => Ok(await _service.GetByCandidateIdAsync(candidateId));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Education education)
        {
            await _service.AddEducationAsync(education);
            return Ok(education);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteEducationAsync(id);
            return NoContent();
        }
    }
}