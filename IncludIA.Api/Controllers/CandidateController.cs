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
    public class CandidateController : ControllerBase
    {
        private readonly CandidateService _service;

        public CandidateController(CandidateService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var candidates = await _service.GetAllAsync(page, size);
            return Ok(candidates);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var candidate = await _service.GetByIdAsync(id);
            if (candidate == null) return NotFound();

            var resource = new
            {
                Data = candidate,
                Links = new[]
                {
                    new { href = Url.Action(nameof(GetById), new { id }), rel = "self", method = "GET" },
                    new { href = Url.Action(nameof(Update), new { id }), rel = "update", method = "PUT" },
                    new { href = Url.Action(nameof(Delete), new { id }), rel = "delete", method = "DELETE" },
                    new { href = Url.Action("GetByCandidateId", "Experience", new { candidateId = id }), rel = "experiences", method = "GET" }
                }
            };

            return Ok(resource);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Candidate candidate)
        {
            try
            {
                await _service.CreateAsync(candidate);
                return CreatedAtAction(nameof(GetById), new { id = candidate.Id }, candidate);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Candidate candidate)
        {
            if (id != candidate.Id) return BadRequest();
            try
            {
                await _service.UpdateAsync(candidate);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}