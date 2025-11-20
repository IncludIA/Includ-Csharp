using Asp.Versioning;
using IncludIA.Application.Service;
using IncludIA.Domain.Entities;
using IncludIA.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace IncludIA.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly MatchService _service;

        public MatchController(MatchService service)
        {
            _service = service;
        }

        [HttpGet("candidate/{candidateId}")]
        public async Task<IActionResult> GetMatchesByCandidate(Guid candidateId)
        {
            var matches = await _service.GetByCandidateIdAsync(candidateId);
            return Ok(matches);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatch([FromBody] Match match)
        {
            await _service.CreateMatchAsync(match);
            return Ok(match);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] MatchStatus status, [FromServices] IncludIA.Domain.Interfaces.IMatchRepository repo)
        {
            return NoContent(); 
        }
    }
}