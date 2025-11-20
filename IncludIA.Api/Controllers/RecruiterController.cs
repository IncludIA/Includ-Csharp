using Asp.Versioning;
using IncludIA.Application.Service;
using IncludIA.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IncludIA.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RecruiterController : ControllerBase
    {
        private readonly RecruiterService _service;

        public RecruiterController(RecruiterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var recruiter = await _service.GetByIdAsync(id);
            if (recruiter == null) return NotFound();
            return Ok(recruiter);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Recruiter recruiter)
        {
            await _service.CreateAsync(recruiter);
            return CreatedAtAction(nameof(GetById), new { id = recruiter.Id }, recruiter);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Recruiter recruiter)
        {
            if (id != recruiter.Id) return BadRequest();
            await _service.UpdateAsync(recruiter);
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